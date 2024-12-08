using System.ComponentModel.DataAnnotations;

namespace eticketing.Http.Requests;

public class CheckoutTicketRequest
{
    [Required]
    public Guid EventId { get; set; }
}
