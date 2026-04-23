using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentTaskTrackerMVC.Models;
using StudentTaskTrackerMVC.Services;

namespace StudentTaskTrackerMVC.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class DashboardController : Controller
    {
        private readonly ApiService _apiService;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(ApiService apiService, UserManager<IdentityUser> userManager)
        {
            _apiService = apiService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<TaskItem> tasks;

            if (User.IsInRole("Admin"))
            {
                tasks = await _apiService.GetTasksAsync();
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                tasks = user == null
                    ? new List<TaskItem>()
                    : await _apiService.GetTasksByUserIdAsync(user.Id);
            }

            var model = new DashboardViewModel
            {
                TotalTasks = tasks.Count,
                CompletedTasks = tasks.Count(t => t.Status == "Completed"),
                PendingTasks = tasks.Count(t => t.Status == "Pending"),
                InProgressTasks = tasks.Count(t => t.Status == "In Progress")
            };

            return View(model);
        }
    }
}