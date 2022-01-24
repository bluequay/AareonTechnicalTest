using AareonTechnicalTest.Exceptions;
using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Repositories
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Note> UpdateAsync(int noteId, Note note, bool saveChanges)
        {
            var existingNote = await GetAsync(noteId);

            if (existingNote is null)
            {
                throw new ResourceNotFoundException($"No note found for id - {noteId}");
            }

            // Ticket id ignored see notes.
            existingNote.Content = note.Content;
            existingNote.PersonId = note.PersonId;

            if (saveChanges)
            {
                await SaveChangesAsync();
            }

            return existingNote;
        }
    }
}
