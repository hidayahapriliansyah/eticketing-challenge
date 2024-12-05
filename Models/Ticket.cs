using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eticketing.Models;

[Table(name: "tickets")]
public class Ticket : Base
{
    [Key]
    [Column(name: "id")]
    public Guid Id { get; set; }

    [Column(name: "code")]
    public string Code { get; set; } = null!;

    [Column(name: "status")]
    public TicketStatus Status { get; set; }

    [ForeignKey("Customer")]
    [Column(name: "user_id")]
    public Guid UserId { get; set; }

    [ForeignKey("Event")]
    [Column(name: "event_id")]
    public Guid EventId { get; set; }

    public Customer User { get; set; } = null!;
    public Event Event { get; set; } = null!;

    public enum TicketStatus
    {
        Pending,
        Expired,
        Confirmed,
        Cancelled,
    }
}
