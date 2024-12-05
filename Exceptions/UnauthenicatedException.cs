using System.Net;
using eticketing.Exceptions;

namespace eticketing.Exceptions;

public class UnauthenticatedException(string message = "Unauthenticated")
    : ApiCustomException(message, HttpStatusCode.Unauthorized);
