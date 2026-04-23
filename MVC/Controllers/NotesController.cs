using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentTaskTrackerMVC.Models;
using StudentTaskTrackerMVC.Services;

namespace StudentTaskTrackerMVC.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly ApiService _apiService;
        private readonly UserManager<IdentityUser> _userManager;

        public NotesController(ApiService apiService, UserManager<IdentityUser> userManager)
        {
            _apiService = apiService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int taskId)
        {
            var task = await _apiService.GetTaskByIdAsync(taskId);

            if (task == null)
            {
                return NotFound();
            }

            if (User.IsInRole("User"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (task.UserId != user?.Id)
                {
                    return Forbid();
                }
            }

            var notes = await _apiService.GetNotesByTaskIdAsync(taskId);

            var model = new TaskNotesViewModel
            {
                Task = task,
                Notes = notes
            };

            return View(model);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> AddNote(int taskId, string newNoteContent)
        {
            var task = await _apiService.GetTaskByIdAsync(taskId);

            if (task == null)
            {
                return NotFound();
            }

            if (User.IsInRole("User"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (task.UserId != user?.Id)
                {
                    return Forbid();
                }
            }

            if (!string.IsNullOrWhiteSpace(newNoteContent))
            {
                var note = new Note
                {
                    Content = newNoteContent,
                    TaskItemId = taskId
                };

                await _apiService.CreateNoteAsync(note);
            }

            return RedirectToAction("Index", new { taskId });
        }
    }
}