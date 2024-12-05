using System.Net;

namespace eticketing.Exceptions;

public class BadRequestException(string message = "Bad Request")
    : ApiCustomException(message, HttpStatusCode.BadRequest);
