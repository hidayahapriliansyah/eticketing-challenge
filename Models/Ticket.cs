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

    [Column(name: "expired_at")]
    public DateTime ExpiredAt { get; set; }

    public Customer User { get; set; } = null!;
    public Event Event { get; set; } = null!;

    public enum TicketStatus
    {
        Pending = 0,
        Expired = 1,
        Confirmed = 2,
        Cancelled = 3,
    }
}
