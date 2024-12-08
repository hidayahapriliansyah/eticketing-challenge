using eticketing.Exceptions;
using eticketing.Http.Requests;
using eticketing.Http.Responses;
using eticketing.Infrastructure.Repository;
using eticketing.Models;
using Microsoft.AspNetCore.Mvc;

namespace eticketing.Application.Services;

public class TicketService(TicketRepository ticketRepository, EventRepository eventRepository)
{
    private readonly TicketRepository _ticketRepository = ticketRepository;
    private readonly EventRepository _eventRepository = eventRepository;

    public async Task<ApiResponse<CheckoutTicketResponse>> CreateTicket(
        CheckoutTicketRequest request,
        Guid customerId
    )
    {
        var eventItem =
            await _eventRepository.GetEventByIdAsync(request.EventId)
            ?? throw new NotFoundException("Event is not found");

        var ticketEntity = new Ticket
        {
            Id = Guid.NewGuid(),
            Code = GenerateTicketCode(),
            EventId = request.EventId,
            UserId = customerId,
            ExpiredAt = eventItem.EventDate.AddDays(1),
            Status = TicketStatus.Pending,
        };

        var createdTicket = await _ticketRepository.CreateTicket(ticketEntity);

        var response = new ApiResponse<CheckoutTicketResponse>
        {
            Success = true,
            Message = "success checkout ticket",
            Data = new CheckoutTicketResponse { Id = createdTicket.Id },
        };

        return response;
    }

    public async Task<ApiResponse<GetTicketDetailResponse>> GetTicketDetailWithUserAndEvent(
        Guid ticketId
    )
    {
        var ticket =
            await _ticketRepository.GetTicketDetailWithEventAndUser(ticketId)
            ?? throw new NotFoundException("Ticket is not found.");
        var response = new ApiResponse<GetTicketDetailResponse>
        {
            Success = true,
            Message = "success to get ticket detail",
            Data = new GetTicketDetailResponse
            {
                Ticket = new TicketDetailDTO
                {
                    Id = ticket.Id,
                    Status = ticket.Status,
                    ExpiredAt = ticket.ExpiredAt,
                    User = new UserDTO
                    {
                        Id = ticket.User.Id,
                        Email = ticket.User.Email,
                        Name = ticket.User.Name,
                    },
                    Event = new TicketEventDetailDTO
                    {
                        Id = ticket.Event.Id,
                        AdditionalInfo = ticket.Event.AdditionalInfo ?? "",
                        Description = ticket.Event.Description,
                        EventDate = ticket.Event.EventDate,
                        Location = ticket.Event.Location,
                        MaxParticipants = ticket.Event.MaxParticipants,
                        Name = ticket.Event.Name,
                        Status = ticket.Event.Status,
                        TicketPrice = ticket.Event.TicketPrice,
                    },
                },
            },
        };

        return response;
    }

    public async Task<ApiResponse<object>> UpdateTicketStatus(
        UpdateTicketRequest request,
        Guid ticketId
    )
    {
        var ticket =
            await _ticketRepository.FindTicket(ticketId)
            ?? throw new NotFoundException("Ticket is not found.");
        await _ticketRepository.UpdateTicketStatus(request, ticket);
        var response = new ApiResponse<object>
        {
            Success = true,
            Message = "success to update ticket status",
        };
        return response;
    }

    public async Task<ApiResponse<GetTicketsResponse>> GetShortTicketList(
        IndexRequest request,
        Guid? customerId
    )
    {
        var (tickets, totalData, totalPages) = await _ticketRepository.GetShortTicketList(
            request,
            customerId
        );

        var response = new ApiResponse<GetTicketsResponse>
        {
            Success = true,
            Message = "success to get tickets",
            Data = new GetTicketsResponse { Tickets = tickets },
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

    public string GenerateTicketCode()
    {
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
        return $"EVT-{timestamp}";
    }
}
