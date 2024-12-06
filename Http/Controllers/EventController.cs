using System.Text.Json.Serialization;
using eticketing.Application.Services;
using eticketing.Http.Filters;
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
        bool isAdmin = CheckIsAdmin(user);
        var response = await _eventService.GetEventsAsync(request, isAdmin);
        return Ok(response);
    }

    [HttpPost]
    [RoleAuthorize("Admin")]
    public async Task<ActionResult<ApiResponse<CreateEventResponse>>> CreateEvent(
        [FromBody] CreateEventRequest request
    )
    {
        Console.WriteLine("Controller Create Event Request Invoked ...");

        var response = await _eventService.CreateEventAsync(request);

        Console.WriteLine("sebelum kirim response nih Controller Create Event Request Invoked ...");

        return CreatedAtAction("GetEvents", new { id = response.Data!.Event.Id }, response);
    }

    [HttpPost("oy")]
    public ActionResult TestAja([FromBody] RandomType request)
    {
        Console.WriteLine("ok");
        Console.WriteLine("request => " + request);
        return Ok();
    }

    private bool CheckIsAdmin(UserAccessTokenData? user)
    {
        return (user == null || user.Role != Roles.Admin) ? false : true;
    }
}

public class RandomType
{
    public required string Title { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EventStatus Status { get; set; }
}
