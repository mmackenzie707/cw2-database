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
        public DbSet<Explorations> Explorations { get; set; }
        public DbSet<TrailInformation> TrailInformation { get; set; }
        public DbSet<IsAuthorized> IsAuthorized { get; set; }

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
            modelBuilder.Entity<Explorations>()
                .ToTable("Explorations")
                .Property(e => e.ExplorationID)
                .UseIdentityColumn(4000, 1); // Ensure the identity column starts at 4000

            modelBuilder.Entity<Explorations>()
                .Property(e => e.CompletionTime)
                .HasColumnName("completionTime")
                .HasColumnType("nvarchar(10)");

            modelBuilder.Entity<Explorations>()
                .Property(e => e.CompletionDate)
                .HasColumnName("completionDate")
                .HasColumnType("datetime2");

            // Configure foreign key relationship for Exploration
            modelBuilder.Entity<Explorations>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Explorations>()
                .HasOne(e => e.Trail)
                .WithMany()
                .HasForeignKey(e => e.TrailID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure TrailInformation entity
            modelBuilder.Entity<TrailInformation>()
                .ToTable("Trail_Information")
                .Property(t => t.TrailID)
                .HasColumnName("trailID");

            // Configure IsAuthorized entity
            modelBuilder.Entity<IsAuthorized>()
                .ToTable("IsAuthorized")
                .HasOne(ia => ia.User)
                .WithMany()
                .HasForeignKey(ia => ia.UserID);
        }
    }
}