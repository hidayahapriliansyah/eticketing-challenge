using eticketing.Http.Requests;
using eticketing.Http.Responses;
using eticketing.Infrastructure.Database;
using eticketing.Models;
using Microsoft.EntityFrameworkCore;

namespace eticketing.Infrastructure.Repository;

public class EventRepository(ETicketingDbContext dbContext)
{
    private ETicketingDbContext _dbContext = dbContext;

    public async Task<(List<EventDTO>, int TotalData, int TotalPages)> GetEventAsync(
        GetEventsRequest request,
        bool isAdmin
    )
    {
        var query = _dbContext.Set<Event>().AsQueryable();

        query = isAdmin
            ? query.Where(e => e.Status == request.Status)
            : query = query.Where(e => e.Status == EventStatus.Published);
        if (request.Date != null)
        {
            query = query.Where(e => e.EventDate == request.Date);
        }

        int totalData = await query.CountAsync();

        int totalPages = (int)Math.Ceiling((double)totalData / request.Limit);

        int skip = (request.Page - 1) * request.Limit;
        query = query.Skip(skip).Take(request.Limit);

        var events = await query
            .Select(e => new EventDTO
            {
                Id = e.Id,
                Name = e.Name,
                EventDate = e.EventDate,
                Location = e.Location,
                TicketPrice = e.TicketPrice,
                MaxParticipants = e.MaxParticipants,
                Status = e.Status.ToString(),
            })
            .ToListAsync();

        return (events, totalData, totalPages);
    }

    public async Task<Event> CreateEventAsync(Event eventEntity)
    {
        _dbContext.Set<Event>().Add(eventEntity);
        await _dbContext.SaveChangesAsync();
        return eventEntity;
    }

    public async Task<Event?> GetEventByIdAsync(Guid eventId)
    {
        return await _dbContext.Event.FirstOrDefaultAsync(e => e.Id == eventId);
    }
}
