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
        var response = await _eventService.CreateEventAsync(request);
        return CreatedAtAction("GetEvents", new { id = response.Data!.Event.Id }, response);
    }

    [HttpGet("{eventId}")]
    public async Task<ActionResult<ApiResponse<GetEventByIdResponse>>> GetEventByIdAsync(
        Guid eventId
    )
    {
        var user = HttpContext.Items["User"] as UserAccessTokenData;
        var isAdmin = CheckIsAdmin(user);
        var response = await _eventService.GetEventByIdAsync(eventId, isAdmin);
        return Ok(response);
    }

    [HttpPut("{eventId}")]
    [RoleAuthorize("Admin")]
    public async Task<ActionResult<ApiResponse<UpdateEventResponse>>> UpdateEventAsync(
        Guid eventId,
        [FromBody] UpdateEventRequest request
    )
    {
        var response = await _eventService.UpdateEventAsync(eventId, request);
        return Ok(response);
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
