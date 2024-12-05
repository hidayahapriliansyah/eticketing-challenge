using eticketing.Models;
using Microsoft.AspNetCore.Mvc;

namespace eticketing.Http.Requests;

public class GetEventsRequest : IndexRequest
{
    [FromQuery(Name = "status")]
    public EventStatus? Status { get; set; }

    [FromQuery(Name = "date")]
    public DateTime? Date { get; set; }
}
