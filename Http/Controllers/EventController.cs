using eticketing.Application.Services;
using eticketing.Http.Requests;
using eticketing.Http.Responses;
using eticketing.Infrastructure.Database;
using eticketing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eticketing.Http.Controllers;

[Route("api/events")]
[ApiController]
public class EventController(EventService eventService) : ControllerBase
{
    private readonly EventService _eventService = eventService;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetEventResponse>>> GetEvents(
        GetEventsRequest request
    )
    {
        var user = HttpContext.Items["User"] as UserAccessTokenData;
        bool isAdmin = _checkIsAdmin(user);
        var response = await _eventService.GetEventsAsync(request, isAdmin);
        return Ok(response);
    }

    private bool _checkIsAdmin(UserAccessTokenData? user)
    {
        return (user == null || user.Role != Roles.Admin) ? false : true;
    }
}
