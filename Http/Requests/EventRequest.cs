using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using eticketing.Models;
using Microsoft.AspNetCore.Mvc;

namespace eticketing.Http.Requests;

public class CreateEventRequest
{
    [FromBody]
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }

    [Required]
    [StringLength(3000, MinimumLength = 1)]
    public required string Description { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public required DateTime EventDate { get; set; }

    [Required]
    [StringLength(200)]
    public required string Location { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int MaxParticipants { get; set; }

    [StringLength(500)]
    public string? AdditionalInfo { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int TicketPrice { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EventStatus Status { get; set; }
}
