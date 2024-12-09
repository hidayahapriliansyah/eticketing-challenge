using eticketing.Application.Services;
using eticketing.Http.Requests;
using eticketing.Infrastructure.Repository;
using eticketing.Models;
using Quartz;

namespace eticketing.Infrastructure.Job;

public class UpdateTicketExpiredJob(TicketService ticketService, TicketRepository ticketRepository)
    : IJob
{
    private readonly TicketService _ticketService = ticketService;
    private readonly TicketRepository _ticketRepository = ticketRepository;

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Starting to update expired tickets at: " + DateTime.Now);

        var expiredTickets = await _ticketRepository.GetUnupdatedExpiredTicket();

        foreach (var ticket in expiredTickets)
        {
            var request = new UpdateTicketRequest { Status = TicketStatus.Expired };
            await _ticketService.UpdateTicketStatus(request, ticket.Id);
            Console.WriteLine($"Ticket {ticket.Id} status updated to 'Expired'");
        }

        Console.WriteLine("Finished updating expired tickets at: " + DateTime.Now);
    }
}
