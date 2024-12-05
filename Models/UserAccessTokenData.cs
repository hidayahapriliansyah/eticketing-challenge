namespace eticketing.Models;

public record UserAccessTokenData(Guid Id, Roles Role);

public enum Roles
{
    Admin,
    Customer,
}
