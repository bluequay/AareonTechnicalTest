using AareonTechnicalTest.Models;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        public Task<Note> UpdateAsync(int noteId, Note note, bool saveChanges = false);
    }
}
