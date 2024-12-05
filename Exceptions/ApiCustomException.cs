using System.Net;

namespace eticketing.Exceptions;

public class ApiCustomException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; set; } = statusCode;
}
