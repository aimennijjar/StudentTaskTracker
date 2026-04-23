namespace StudentTaskTrackerMVC.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalRoles { get; set; }
        public List<UserRoleViewModel> Users { get; set; } = new();
    }

    public class UserRoleViewModel
    {
        public string Id { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
    }
}