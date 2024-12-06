using System.Net;

namespace eticketing.Exceptions;

public class NotFoundException(string message = "Not foudn")
    : ApiCustomException(message, HttpStatusCode.NotFound);
