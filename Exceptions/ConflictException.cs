using System.Net;

namespace eticketing.Exceptions;

public class ConflictException(string message = "Conflict")
    : ApiCustomException(message, HttpStatusCode.Conflict);
