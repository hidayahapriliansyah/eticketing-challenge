using eticketing.Models;

namespace eticketing.Http.Responses;

public class EventDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public string Location { get; set; } = null!;
    public int TicketPrice { get; set; }
    public int MaxParticipants { get; set; }
    public string Status { get; set; } = null!;
}

public class GetEventResponse
{
    public List<EventDTO> Events { get; set; } = [];
}

public class CreateEventResponse
{
    public EventDTO Event { get; set; } = null!;
}

public class GetEventByIdResponse : EventDTO { }
