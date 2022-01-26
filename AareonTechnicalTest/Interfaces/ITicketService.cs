using AareonTechnicalTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Interfaces
{
    public interface ITicketService
    {
        ValueTask<Ticket> GetAsync(int id);
        
        Task<List<Ticket>> GetAllAsync();
        
        Task<Ticket> CreateAsync(Ticket ticket);
        
        Task<Ticket> UpdateAsync(int ticketId, Ticket ticket);

        Task DeleteAsync(int id);
    }
}
