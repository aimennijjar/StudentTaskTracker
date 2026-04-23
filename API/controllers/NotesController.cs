using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTaskTrackerAPI.Data;
using StudentTaskTrackerAPI.Models;

namespace StudentTaskTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        [HttpGet("task/{taskItemId}")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesByTask(int taskItemId)
        {
            return await _context.Notes
                .Where(n => n.TaskItemId == taskItemId)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNote), new { id = note.NoteId }, note);
        }
    }
}