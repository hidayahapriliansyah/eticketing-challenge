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
            Data = new CreateEventResponse
            {
                Event = new EventDTO
                {
                    Id = createdEvent.Id,
                    EventDate = createdEvent.EventDate,
                    Location = createdEvent.Location,
                    MaxParticipants = createdEvent.MaxParticipants,
                    Name = createdEvent.Name,
                    Status = createdEvent.Status.ToString(),
                    TicketPrice = createdEvent.TicketPrice,
                },
            },
            Pagination = null,
        };

        return response;
    }
}
