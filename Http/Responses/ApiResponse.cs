using System.Net;

namespace eticketing.Http.Responses;

public class ApiResponse<T>
{
    public required bool Success { get; set; }
    public required string Message { get; set; }
    public T? Data { get; set; }

    public Pagination? Pagination { get; set; }
}

public class Pagination
{
    public int TotalData { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
}
