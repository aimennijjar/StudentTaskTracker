using StudentTaskTrackerMVC.Services;

namespace StudentTaskTrackerMVC.Models
{
    public class GroupedTasksViewModel
    {
        public string UserEmail { get; set; } = "";
        public List<TaskItem> Tasks { get; set; } = new();
    }

    public class TasksPageViewModel
    {
        public List<GroupedTasksViewModel> GroupedTasks { get; set; } = new();
        public bool IsAdmin { get; set; }
        public bool IsViewer { get; set; }
        public string? SearchEmail { get; set; }
        public string? Message { get; set; }
    }
}