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
    public async Task<ActionResult<ApiResponse<GetEventResponse>>> GetEvents()
    {
        var events = await _dbContext
            .Set<Event>()
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
        };

        return Ok(response);
    }
}
