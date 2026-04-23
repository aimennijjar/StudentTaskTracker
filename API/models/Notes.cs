using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTaskTrackerAPI.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int TaskItemId { get; set; }

        [ForeignKey("TaskItemId")]
        public TaskItem? TaskItem { get; set; }
    }
}