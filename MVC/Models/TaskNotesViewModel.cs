using StudentTaskTrackerMVC.Services;

namespace StudentTaskTrackerMVC.Models
{
    public class TaskNotesViewModel
    {
        public TaskItem? Task { get; set; }
        public List<Note> Notes { get; set; } = new();
        public string NewNoteContent { get; set; } = "";
    }
}