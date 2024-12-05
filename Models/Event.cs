using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eticketing.Models;

[Table(name: "events")]
public class Event : Base
{
    [Key]
    [Column(name: "id")]
    public Guid Id { get; set; }

    [Column(name: "name")]
    public string Name { get; set; } = null!;

    [Column(name: "description")]
    public string Description { get; set; } = null!;

    [Column(name: "event_date")]
    public DateTime EventDate { get; set; }

    [Column(name: "location")]
    public string Location { get; set; } = null!;

    [Column(name: "max_participants")]
    public int MaxParticipants { get; set; }

    [Column(name: "additional_info")]
    public string? AdditionalInfo { get; set; }

    [Column(name: "ticket_price")]
    public int TicketPrice { get; set; }

    [Column(name: "status")]
    public EventStatus Status { get; set; }

    public List<Ticket> Tickets { get; set; } = [];

    public enum EventStatus
    {
        Published,
        Unpublished,
    }
}
