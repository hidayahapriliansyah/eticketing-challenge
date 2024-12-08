using eticketing.Application.Services;
using eticketing.Exceptions;
using eticketing.Http.Filters;
using eticketing.Http.Requests;
using eticketing.Http.Responses;
using eticketing.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class TicketController(TicketService ticketService) : ControllerBase
{
    public readonly TicketService _ticketService = ticketService;

    [HttpPost("checkout")]
    [RoleAuthorize("Customer")]
    public async Task<ActionResult<ApiResponse<CheckoutTicketResponse>>> CheckoutTicket(
        [FromBody] CheckoutTicketRequest request
    )
    {
        var user = HttpContext.Items["User"] as UserAccessTokenData;
        var response = await _ticketService.CreateTicket(request, user!.Id);
        return CreatedAtAction(nameof(GetTicketDetail), new { Id = response.Data!.Id }, response);
    }

    [HttpGet("tickets")]
    [RoleAuthorize("Admin", "Customer")]
    public async Task<ActionResult<ApiResponse<GetTicketsResponse>>> GetTickets(
        IndexRequest request
    )
    {
        var user = HttpContext.Items["User"] as UserAccessTokenData;
        bool isAdmin = user!.Role == Roles.Admin;
        return isAdmin
            ? await GetTicketsForAdmin(request)
            : await GetTicketsForCustomer(request, user.Id);
    }

    public async Task<ActionResult<ApiResponse<GetTicketsResponse>>> GetTicketsForCustomer(
        IndexRequest request,
        Guid customerId
    )
    {
        return await _ticketService.GetShortTicketList(request, customerId);
    }

    public async Task<ActionResult<ApiResponse<GetTicketsResponse>>> GetTicketsForAdmin(
        IndexRequest request
    )
    {
        return await _ticketService.GetShortTicketList(request, null);
    }

    [HttpGet("tickets/{ticketId}")]
    [RoleAuthorize("Admin", "Customer")]
    public async Task<ActionResult<ApiResponse<GetTicketDetailResponse>>> GetTicketDetail(
        Guid ticketId
    )
    {
        var user = HttpContext.Items["User"] as UserAccessTokenData;
        var response = await _ticketService.GetTicketDetailWithUserAndEvent(ticketId);
        bool isCustomer = user!.Role == Roles.Customer;

        if (isCustomer && (user.Id != response.Data!.Ticket.User.Id))
        {
            throw new ForbiddenException();
        }

        return response;
    }

    [HttpPut("tickets/{ticketId}")]
    [RoleAuthorize("Admin")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateTicketStatus(
        [FromBody] UpdateTicketRequest request,
        Guid ticketId
    )
    {
        var response = await _ticketService.UpdateTicketStatus(request, ticketId);
        return Ok(response);
    }
}
