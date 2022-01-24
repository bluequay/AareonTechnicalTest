using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Controllers
{
    /// <summary>
    /// Note Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {

        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        /// <summary>
        /// Get All Notes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAsync()
        {
            var notes = await _noteService.GetAllAsync();

            if(notes.Count > 0)
            {
                return Ok(notes);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Get a note
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetAsync(int id)
        {
            var note = await _noteService.GetAsync(id);

            return Ok(note);
           
        }

        /// <summary>
        /// Create a new note
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Note>> PostAsync(Note note)
        {
            var createdNote = await _noteService.CreateAsync(note);

            return CreatedAtAction("Get", new { id = createdNote.Id }, createdNote);
        }

        /// <summary>
        /// Update an existing note
        /// </summary>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Note>> PutAsync(int id, Note note)
        {
            var updatedNote = await _noteService.UpdateAsync(id, note);

            return Ok(updatedNote);
        }

        /// <summary>
        /// Delete a note
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}/person/{personId}")]
        public async Task<ActionResult> Delete(int id, int personId)
        {
            // I will cover this more in my notes but clearly this is not how I would create this resource
            // in a real system the person\user would always be always resolved from authentication
            await _noteService.DeleteAsync(personId, id);

            return NoContent();
        }
    }
}
