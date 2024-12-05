using System.Net;

namespace eticketing.Http.Responses;

public class ApiResponse<T>(
    HttpStatusCode statusCode,
    T? data,
    string message,
    object? errors = null
)
{
    public int StatusCode { get; } = (int)statusCode;
    public T? Data { get; } = data;
    public string Message { get; } = message;
    public object? Errors { get; } = errors;
}
