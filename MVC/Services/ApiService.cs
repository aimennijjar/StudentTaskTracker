using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace StudentTaskTrackerMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskItem>> GetTasksAsync()
        {
            var response = await _httpClient.GetAsync("api/TaskItems");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TaskItem>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<List<TaskItem>> GetTasksByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"api/TaskItems/user/{userId}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TaskItem>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task CreateTaskAsync(TaskItem task)
        {
            var json = JsonSerializer.Serialize(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/TaskItems", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/TaskItems/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TaskItem>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            var json = JsonSerializer.Serialize(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/TaskItems/{task.TaskItemId}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/TaskItems/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<TaskItem>> SearchTasksAsync(string? status, string? priority, string? title)
        {
            var url = "api/TaskItems/search?";

            if (!string.IsNullOrEmpty(status))
                url += $"status={status}&";

            if (!string.IsNullOrEmpty(priority))
                url += $"priority={priority}&";

            if (!string.IsNullOrEmpty(title))
                url += $"title={title}&";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<TaskItem>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            var response = await _httpClient.GetAsync("api/Projects");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Project>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<Quote?> GetRandomQuoteAsync()
{
    var response = await _httpClient.GetAsync("api/Quotes/random");
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();

    return JsonSerializer.Deserialize<Quote>(json,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
}

public async Task<List<Note>> GetNotesByTaskIdAsync(int taskItemId)
{
    var response = await _httpClient.GetAsync($"api/Notes/task/{taskItemId}");
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<List<Note>>(json,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
}

public async Task CreateNoteAsync(Note note)
{
    var json = JsonSerializer.Serialize(note);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await _httpClient.PostAsync("api/Notes", content);
    response.EnsureSuccessStatusCode();
}
    }

    public class TaskItem
    {
        public int TaskItemId { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "";
        public string Priority { get; set; } = "";
        public int ProjectId { get; set; }
        public string? UserId { get; set; }
        public string? ProjectName { get; set; }
    }

    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }

    public class Quote
{
    public int QuoteId { get; set; }
    public string Text { get; set; } = "";
}

public class Note
{
    public int NoteId { get; set; }
    public string Content { get; set; } = "";
    public int TaskItemId { get; set; }
}
}