using eticketing.Http.Requests;
using eticketing.Http.Responses;
using eticketing.Infrastructure.Database;
using eticketing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eticketing.Http.Controllers;

[Route("api/events")]
[ApiController]
public class EventController(ETicketingDbContext dbContext) : ControllerBase
{
    private readonly ETicketingDbContext _dbContext = dbContext;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetEventResponse>>> GetEvents(
        GetEventsRequest request
    )
    {
        var query = _dbContext.Set<Event>().AsQueryable();

        var user = HttpContext.Items["User"] as UserAccessTokenData;
        bool isAdmin = _checkIsAdmin(user);

        query = isAdmin
            ? query.Where(e => e.Status == request.Status)
            : query = query.Where(e => e.Status == EventStatus.Published);
        if (request.Date != null)
        {
            query = query.Where(e => e.EventDate == request.Date);
        }

        int totalData = await query.CountAsync();

        int totalPages = (int)Math.Ceiling((double)totalData / request.Limit);

        int skip = (request.Page - 1) * request.Limit;
        query = query.Skip(skip).Take(request.Limit);

        var events = await query
            .Select(e => new EventDTO
            {
                Id = e.Id,
                Name = e.Name,
                EventDate = e.EventDate,
                Location = e.Location,
                TicketPrice = e.TicketPrice,
                MaxParticipants = e.MaxParticipants,
                Status = e.Status.ToString(),
            })
            .ToListAsync();

        var response = new ApiResponse<GetEventResponse>
        {
            Success = true,
            Message = "success to get events",
            Data = new GetEventResponse { Events = events },
            Pagination = new Pagination
            {
                TotalData = totalData,
                TotalPages = totalPages,
                PageSize = request.Limit,
                CurrentPage = request.Page,
            },
        };

        return Ok(response);
    }

    private bool _checkIsAdmin(UserAccessTokenData? user)
    {
        return (user == null || user.Role != Roles.Admin) ? false : true;
    }
}
