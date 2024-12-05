using System.Net;

namespace eticketing.Exceptions;

public class ForbiddenException(string message = "Forbidden")
    : ApiCustomException(message, HttpStatusCode.Forbidden);
