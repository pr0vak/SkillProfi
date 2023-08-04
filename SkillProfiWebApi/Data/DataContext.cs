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
    }
}
