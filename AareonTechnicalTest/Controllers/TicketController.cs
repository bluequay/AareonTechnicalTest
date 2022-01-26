using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Controllers
{
    /// <summary>
    /// Ticket Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Get All Tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAsync()
        {
            var tickets = await _ticketService.GetAllAsync();

            if(tickets.Count > 0)
            {
                return Ok(tickets);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Get a ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetAsync(int id)
        {
            var ticket = await _ticketService.GetAsync(id);

            return Ok(ticket);
           
        }

        /// <summary>
        /// Create a new ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostAsync(Ticket ticket)
        {
            var createdTicket = await _ticketService.CreateAsync(ticket);

            return CreatedAtAction("Get", new { id = createdTicket.Id }, createdTicket);
        }

        /// <summary>
        /// Update an existing ticket
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Ticket>> PutAsync(int id, Ticket ticket)
        {
            var updatedTicket = await _ticketService.UpdateAsync(id, ticket);

            return Ok(updatedTicket);
        }

        /// <summary>
        /// Delete a ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _ticketService.DeleteAsync(id);

            return NoContent();
        }
    }
}
