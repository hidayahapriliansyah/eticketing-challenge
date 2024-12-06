using Azure;
using eticketing.Exceptions;
using eticketing.Http.Requests;
using eticketing.Http.Responses;
using eticketing.Infrastructure.Repository;
using eticketing.Models;

namespace eticketing.Application.Services;

public class EventService(EventRepository eventRepository)
{
    private EventRepository _eventRepository = eventRepository;

    public async Task<ApiResponse<GetEventResponse>> GetEventsAsync(
        GetEventsRequest request,
        bool isAdmin
    )
    {
        var (events, totalData, totalPages) = await _eventRepository.GetEventAsync(
            request,
            isAdmin
        );

        var response = new ApiResponse<GetEventResponse>
        {
            Success = true,
            Message = "success to get events",
            Data = new GetEventResponse { Events = events },
            Pagination = new Pagination
            {
                TotalData = totalData,
                TotalPages = totalPages,
                CurrentPage = request.Page,
                PageSize = request.Limit,
            },
        };
        return response;
    }

    public async Task<ApiResponse<CreateEventResponse>> CreateEventAsync(CreateEventRequest request)
    {
        var eventEntity = new Event
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            EventDate = request.EventDate,
            Location = request.Location,
            AdditionalInfo = request.AdditionalInfo,
            MaxParticipants = request.MaxParticipants,
            Status = request.Status,
            TicketPrice = request.TicketPrice,
        };

        var createdEvent = await _eventRepository.CreateEventAsync(eventEntity);

        var response = new ApiResponse<CreateEventResponse>
        {
            Success = true,
            Message = "success to create event",
            Data = new CreateEventResponse { Event = ToEventDTO(createdEvent) },
            Pagination = null,
        };

        return response;
    }

    public async Task<ApiResponse<GetEventByIdResponse>> GetEventByIdAsync(
        Guid eventId,
        bool isAdmin
    )
    {
        var eventItem = await _eventRepository.GetEventByIdAsync(eventId);

        if ((eventItem == null) || (!isAdmin && eventItem.Status == EventStatus.Unpublished))
        {
            throw new NotFoundException("Event id is not found.");
        }

        var response = new ApiResponse<GetEventByIdResponse>
        {
            Success = true,
            Message = "success to get event detail",
            Data = new GetEventByIdResponse
            {
                Id = eventItem.Id,
                EventDate = eventItem.EventDate,
                Location = eventItem.Location,
                MaxParticipants = eventItem.MaxParticipants,
                Name = eventItem.Name,
                Status = eventItem.Status.ToString(),
                TicketPrice = eventItem.TicketPrice,
            },
        };
        return response;
    }

    public async Task<ApiResponse<UpdateEventResponse>> UpdateEventAsync(
        Guid eventId,
        UpdateEventRequest request
    )
    {
        var updatedEvent =
            await _eventRepository.UpdateEventAsyncOrReturnNull(eventId, request)
            ?? throw new NotFoundException("Event is not found");

        var response = new ApiResponse<UpdateEventResponse>
        {
            Success = true,
            Message = "success to update event",
            Data = new UpdateEventResponse
            {
                Event = new EventDTO
                {
                    Id = updatedEvent.Id,
                    Name = updatedEvent.Name,
                    EventDate = updatedEvent.EventDate,
                    Location = updatedEvent.Location,
                    MaxParticipants = updatedEvent.MaxParticipants,
                    TicketPrice = updatedEvent.TicketPrice,
                    Status = updatedEvent.Status.ToString(),
                },
            },
        };

        return response;
    }

    public async Task<ApiResponse<object>> SoftDeleteEventByIdAsync(Guid eventId)
    {
        var eventItem =
            await _eventRepository.GetNotDeletedEventById(eventId)
            ?? throw new NotFoundException("Event is not found");

        await _eventRepository.SoftDeleteEvent(eventItem);

        var response = new ApiResponse<object>
        {
            Success = true,
            Message = "success to delete event",
        };
        return response;
    }

    public EventDTO ToEventDTO(Event eventItem)
    {
        return new EventDTO
        {
            Id = eventItem.Id,
            EventDate = eventItem.EventDate,
            Location = eventItem.Location,
            MaxParticipants = eventItem.MaxParticipants,
            Name = eventItem.Name,
            Status = eventItem.Status.ToString(),
            TicketPrice = eventItem.TicketPrice,
        };
    }
}
