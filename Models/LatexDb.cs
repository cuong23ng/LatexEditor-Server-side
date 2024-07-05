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

            modelBuilder.Entity<Project>()
                .Property(d => d.LastModified)
                .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<File>()
                .HasKey(f => new { f.FileName, f.ProjectId, f.DataType });
            modelBuilder.Entity<File>()
                .HasOne(f => f.Project)
                .WithMany(p => p.Files)
                .HasForeignKey(f => f.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}