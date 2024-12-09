using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace eticketing.Http.Requests;

public class IndexRequest
{
    [FromQuery(Name = "limit")]
    public int Limit { get; set; } = 10;

    [FromQuery(Name = "page")]
    public int Page { get; set; } = 1; // Nilai default 1
}
