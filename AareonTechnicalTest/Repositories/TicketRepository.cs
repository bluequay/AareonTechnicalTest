using AareonTechnicalTest.Exceptions;
using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Repositories
{ 

    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationContext context) : base(context)
        { }

        public async Task<Ticket> UpdateAsync(int ticketId, Ticket ticket, bool saveChanges)
        {
            var existingTicket = await GetAsync(ticketId);

            if (existingTicket is null)
            {
                throw new ResourceNotFoundException($"No ticket found for id - {ticketId}");
            }

            existingTicket.Content = ticket.Content;
            existingTicket.PersonId = ticket.PersonId;

            if(saveChanges)
            {
                await SaveChangesAsync();
            }

            return existingTicket;
        }
    }
}
