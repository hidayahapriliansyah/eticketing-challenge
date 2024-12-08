using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using eticketing.Models;

namespace eticketing.Http.Requests;

public class CheckoutTicketRequest
{
    [Required]
    public Guid EventId { get; set; }
}

public class UpdateTicketRequest
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TicketStatus Status { get; set; }
}
