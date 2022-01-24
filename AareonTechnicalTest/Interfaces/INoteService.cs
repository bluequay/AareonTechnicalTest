using AareonTechnicalTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Interfaces
{
    public interface INoteService
    {
        ValueTask<Note> GetAsync(int id);
        
        Task<List<Note>> GetAllAsync();
        
        Task<Note> CreateAsync(Note note);
        
        Task<Note> UpdateAsync(int noteId, Note note);

        Task DeleteAsync(int personId, int id);
    }
}
