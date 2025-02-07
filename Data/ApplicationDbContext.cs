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

            // Configure TrailID to start with 'AA0000' and increment by 1
            modelBuilder.Entity<Exploration>()
                .Property(e => e.TrailID)
                .HasDefaultValueSql("'AA' + RIGHT('0000' + CAST(NEXT VALUE FOR dbo.TrailIDSequence AS VARCHAR(4)), 4)");
        }
    }
}