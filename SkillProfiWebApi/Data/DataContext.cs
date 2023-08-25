using Microsoft.EntityFrameworkCore;
using SkillProfi.DAL.Auth;
using SkillProfi.DAL.Models;

namespace SkillProfiWebApi.Data
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// Заявки.
        /// </summary>
        public DbSet<Request> Requests { get; set; } = default!;

        /// <summary>
        /// Услуги.
        /// </summary>
        public DbSet<Service> Services { get; set; } = default!;

        /// <summary>
        /// Проекты.
        /// </summary>
        public DbSet<Project> Projects { get; set; } = default!;

        /// <summary>
        /// Блоги.
        /// </summary>
        public DbSet<Blog> Blogs { get; set; } = default!;

        /// <summary>
        /// Аккаунты.
        /// </summary>
        public DbSet<Account> Accounts { get; set; } = default!;


        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
            // Создане базы данных, если её нет.
            Database.EnsureCreated();
        }
    }
}
