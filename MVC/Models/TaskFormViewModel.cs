using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTaskTrackerMVC.Services;

namespace StudentTaskTrackerMVC.Models
{
    public class TaskFormViewModel
    {
        public TaskItem Task { get; set; } = new();
        public List<SelectListItem> Projects { get; set; } = new();
    }
}