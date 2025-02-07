using Microsoft.EntityFrameworkCore;
using trailAPI.Models;

namespace trailAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Exploration> Explorations { get; set; }
        public DbSet<TrailInformation> Trail_Information { get; set; } // Add this line

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .Property(u => u.UserID)
                .HasColumnName("id")
                .UseIdentityColumn(1000, 1);

            // Configure Exploration entity
            modelBuilder.Entity<Exploration>()
                .ToTable("Explorations")
                .Property(e => e.ExplorationID)
                .UseIdentityColumn(4000, 1);

            // Configure foreign key relationship
            modelBuilder.Entity<Exploration>()
                .HasOne(e => e.User)
                .WithMany(u => u.Explorations)
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure TrailInformation entity
            modelBuilder.Entity<TrailInformation>()
                .ToTable("Trail_Information")
                .Property(t => t.TrailID)
                .HasColumnName("trailID");
        }
    }
}