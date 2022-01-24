using AareonTechnicalTest.Exceptions;
using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Services
{
    public class TicketService : ITicketService
    {

        private readonly ITicketRepository _ticketRepository;
        private readonly IPersonService _personService;

        public TicketService(ITicketRepository ticketRepository, IPersonService personService)
        {
            _ticketRepository = ticketRepository;
            _personService = personService;
        }

        public async Task<Ticket> CreateAsync(Ticket ticket)
        {

            await ValidatePersonExists(ticket.PersonId);

            await _ticketRepository.AddAsync(ticket, true);

            return ticket;
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await GetAsync(id);

            if(ticket is null)
            {
                throw new ResourceNotFoundException($"No ticket found for id - {id}");
            }

            await _ticketRepository.RemoveAsync(ticket, true);
        }

        public Task<List<Ticket>> GetAllAsync()
        {
            return _ticketRepository.GetAllAsync();
        }

        public async ValueTask<Ticket> GetAsync(int id)
        {
            var ticket = await _ticketRepository.GetAsync(id);

            if(ticket is null)
            {
                throw new ResourceNotFoundException($"No ticket found for id - {id}");
            }

            return ticket;
        }

        public async Task<Ticket> UpdateAsync(int ticketId, Ticket ticket)
        {
            await ValidatePersonExists(ticket.PersonId);
            await _ticketRepository.UpdateAsync(ticketId, ticket, true);
            return ticket;
        }

        private async Task ValidatePersonExists(int personId)
        {
            var person = await _personService.GetAsync(personId);

            if(person is null)
            {
                throw new ResourceNotFoundException($"No person found for id - {personId}"); ;
            }
        }
    }
}
