using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace eticketing.Http.Requests;

public class IndexRequest(int limit = 10, int page = 1)
{
    [FromQuery(Name = "limit")]
    public int Limit { get; set; } = limit;

    [FromQuery(Name = "page")]
    public int Page { get; set; } = page;
}
