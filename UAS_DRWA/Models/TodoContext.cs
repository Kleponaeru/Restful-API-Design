using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Kelas> Kelas { get; set; } 
         public DbSet<Mapel> Mapels { get; set; }
         public DbSet<Guru> Gurus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity mappings or relationships here if needed
        }
    }
}
