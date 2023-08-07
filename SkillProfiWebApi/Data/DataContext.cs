using Microsoft.EntityFrameworkCore;
using SkillProfiWebApi.Models;

namespace SkillProfiWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Request> Requests { get; set; } = default!;

        public DbSet<Service> Services { get; set; } = default!;

        public DbSet<Project> Projects { get; set; } = default!;

        public DbSet<Blog> Blogs{ get; set; } = default!;
    }
}
