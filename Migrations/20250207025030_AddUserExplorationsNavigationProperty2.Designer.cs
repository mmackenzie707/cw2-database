using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using trailAPI.Data;

namespace trailAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250207025030_AddUserExplorationsNavigationProperty2")]
    partial class AddUserExplorationsNavigationProperty2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("trailAPI.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn(1000, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("firstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("lastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.HasKey("UserID");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("trailAPI.Models.Exploration", b =>
                {
                    b.Property<int>("ExplorationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("explorationID")
                        .UseIdentityColumn(4000, 1);

                    b.Property<int>("UserID")
                        .HasColumnType("int")
                        .HasColumnName("userID");

                    b.Property<string>("TrailID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("trailID");

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("completionDate");

                    b.Property<bool>("CompletionStatus")
                        .HasColumnType("bit")
                        .HasColumnName("completionStatus");

                    b.HasKey("ExplorationID");

                    b.HasIndex("UserID");

                    b.HasIndex("TrailID");

                    b.ToTable("Explorations", (string)null);

                    b.HasOne("trailAPI.Models.User", "User")
                        .WithMany("Explorations")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("trailAPI.Models.TrailInformation", "TrailInformation")
                        .WithMany("Explorations")
                        .HasForeignKey("TrailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("TrailInformation");
                });

            modelBuilder.Entity("trailAPI.Models.TrailInformation", b =>
                {
                    b.Property<string>("TrailID")
                        .HasColumnType("int")
                        .HasColumnName("trailID");

                    b.Property<string>("TrailName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("trailName");

                    b.Property<string>("TrailDescription")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("trailDescription");

                    b.Property<string>("TrailLocation")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("trailLocation");

                    b.HasKey("TrailID");

                    b.ToTable("Trail_Information", (string)null);
                });

            modelBuilder.Entity("trailAPI.Models.User", b =>
                {
                    b.Navigation("Explorations");
                });

            modelBuilder.Entity("trailAPI.Models.TrailInformation", b =>
                {
                    b.Navigation("Explorations");
                });
#pragma warning restore 612, 618
        }
    }
}