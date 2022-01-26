using AareonTechnicalTest.Exceptions;
using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Services
{
    public class NoteService : INoteService
    {

        private readonly INoteRepository _noteRepository;
        private readonly ITicketService _ticketService;
        private readonly IPersonService _personService;

        public NoteService(INoteRepository noteRepository, ITicketService ticketService, IPersonService personService)
        {
            _noteRepository = noteRepository;
            _ticketService = ticketService;
            _personService = personService;
        }

        public async Task<Note> CreateAsync(Note note)
        {
            await ValidatePersonExists(note.PersonId);
            await ValidateTicketExists(note.TicketId);

            await _noteRepository.AddAsync(note, true);

            return note;
        }

        public async Task DeleteAsync(int personId, int id)
        {
            var note = await GetAsync(id);

            if(note is null)
            {
                throw new ResourceNotFoundException($"No note found for id - {id}");
            }

            var person = await ValidatePersonExists(personId);

            if (person.IsAdmin || person.Id == note.PersonId)
            {
                await _noteRepository.RemoveAsync(note, true);
            }
            else
            {
                throw new UserNotAuthorisedException("");
            }
        }

        public Task<List<Note>> GetAllAsync()
        {
            return _noteRepository.GetAllAsync();
        }

        public async ValueTask<Note> GetAsync(int id)
        {
            var note = await _noteRepository.GetAsync(id);

            if(note is null)
            {
                throw new ResourceNotFoundException($"No note found for id - {id}");
            }

            return note;
        }

        public async Task<Note> UpdateAsync(int noteId, Note note)
        {
            await ValidatePersonExists(note.PersonId);
            await ValidateTicketExists(note.TicketId);
            var updatedNote = await _noteRepository.UpdateAsync(noteId, note, true);
            return updatedNote;
        }

        private async Task<Person> ValidatePersonExists(int personId)
        {
            var person = await _personService.GetAsync(personId);

            if(person is null)
            {
                throw new ResourceNotFoundException($"No person found for id - {personId}"); ;
            }

            return person;
        }

        private async Task ValidateTicketExists(int ticketId)
        {
            var person = await _ticketService.GetAsync(ticketId);

            if (person is null)
            {
                throw new ResourceNotFoundException($"No ticket found for id - {ticketId}"); ;
            }
        }
    }
}
