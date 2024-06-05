using Microsoft.EntityFrameworkCore;

namespace Hustex_backend.Models
{
    public class LatexDb : DbContext
    {
        
        public LatexDb(DbContextOptions<LatexDb> options) : base(options)
        {
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<File>()
                .HasKey(f => new { f.FileName, f.ProjectId, f.DataType });

            modelBuilder.Entity<Project>()
                .Property(d => d.LastModified)
                .HasDefaultValue(DateTime.Now);
        }
    }
}