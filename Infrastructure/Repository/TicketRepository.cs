using eticketing.Http.Requests;
using eticketing.Http.Responses;
using eticketing.Infrastructure.Database;
using eticketing.Models;
using Microsoft.EntityFrameworkCore;

namespace eticketing.Infrastructure.Repository;

public class TicketRepository(ETicketingDbContext dbContext)
{
    private readonly ETicketingDbContext _dbContext = dbContext;

    public async Task<Ticket> CreateTicket(Ticket ticketEntity)
    {
        _dbContext.Set<Ticket>().Add(ticketEntity);
        await _dbContext.SaveChangesAsync();
        return ticketEntity;
    }

    public async Task<Ticket?> GetTicketDetailWithEventAndUser(Guid ticketId)
    {
        var ticket = await _dbContext
            .Ticket.Include(t => t.Event)
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == ticketId);
        return ticket;
    }

    public async Task<(List<ShortTicketDTO>, int TotalData, int TotalPages)> GetShortTicketList(
        IndexRequest request,
        Guid? customerId
    )
    {
        var query = _dbContext.Set<Ticket>().AsQueryable();

        if (customerId != null)
        {
            query.Where(t => t.UserId == customerId);
        }

        int totalData = await query.CountAsync();

        int totalPages = (int)Math.Ceiling((double)totalData / request.Limit);

        int skip = (request.Page - 1) * request.Limit;
        query = query.Skip(skip).Take(request.Limit);

        var tickets = await query
            .Select(t => new ShortTicketDTO
            {
                Id = t.Id,
                ExpiredAt = t.ExpiredAt,
                Status = t.Status,
                UserId = t.UserId,
            })
            .ToListAsync();

        return (tickets, totalData, totalPages);
    }
}
