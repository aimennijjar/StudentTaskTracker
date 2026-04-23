using Microsoft.EntityFrameworkCore;
using StudentTaskTrackerAPI.Models;

namespace StudentTaskTrackerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}