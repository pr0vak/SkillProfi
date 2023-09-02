using Microsoft.EntityFrameworkCore;
using SkillProfi.DAL.Models;

namespace SkillProfi.Api.Data
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// Заявки.
        /// </summary>
        public DbSet<Request>? Requests { get; set; }

        /// <summary>
        /// Услуги.
        /// </summary>
        public DbSet<Service>? Services { get; set; }

        /// <summary>
        /// Проекты.
        /// </summary>
        public DbSet<Project>? Projects { get; set; }

        /// <summary>
        /// Блоги.
        /// </summary>
        public DbSet<Blog>? Blogs { get; set; }

        /// <summary>
        /// Аккаунты.
        /// </summary>
        public DbSet<Account>? Accounts { get; set; }


        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
            // Создане базы данных, если её нет.
            Database.EnsureCreated();
        }
    }
}
