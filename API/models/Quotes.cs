using System.ComponentModel.DataAnnotations;

namespace StudentTaskTrackerAPI.Models
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }

        [Required]
        public string Text { get; set; } = "";
    }
}