using ETicketing.Migrations;
using eticketing.Models;

namespace eticketing.Http.Responses;

public class CheckoutTicketResponse
{
    public Guid Id { get; set; }
}

public class GetTicketDetailResponse
{
    public TicketDetailDTO Ticket { get; set; } = null!;
}

public class TicketDetailDTO
{
    public Guid Id { get; set; }
    public TicketEventDetailDTO Event { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime ExpiredAt { get; set; }

    public UserDTO User { get; set; } = null!;
}

public class UserDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}

public class TicketEventDetailDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public string Location { get; set; } = null!;
    public int TicketPrice { get; set; }
    public int MaxParticipants { get; set; }
    public string AdditionalInfo { get; set; } = null!;
    public string Status { get; set; } = null!;
}

public class ShortTicketDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime ExpiredAt { get; set; }
}

public class GetTicketsResponse
{
    public List<ShortTicketDTO> Tickets { get; set; } = [];
}
