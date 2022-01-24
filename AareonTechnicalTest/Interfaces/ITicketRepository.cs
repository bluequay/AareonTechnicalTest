using AareonTechnicalTest.Models;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket> 
    {
        public Task<Ticket> UpdateAsync(int ticketId, Ticket ticket, bool saveChanges = false);
    }
}
