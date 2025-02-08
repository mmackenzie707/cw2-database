﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using trailAPI.Data;

#nullable disable

namespace trailAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250207020325_AddExplorationTable4")]
    partial class AddExplorationTable4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("trailAPI.Models.Exploration", b =>
                {
                    b.Property<int>("ExplorationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("explorationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExplorationID"), 4000L);

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("completionDate");

                    b.Property<bool>("CompletionStatus")
                        .HasColumnType("bit")
                        .HasColumnName("completionStatus");

                    b.Property<string>("TrailID")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("trailID")
                        .HasDefaultValueSql("'AA' + RIGHT('0000' + CAST(NEXT VALUE FOR dbo.TrailIDSequence AS VARCHAR(4)), 4)");

                    b.Property<int>("UserID")
                        .HasColumnType("int")
                        .HasColumnName("userID");

                    b.HasKey("ExplorationID");

                    b.HasIndex("UserID");

                    b.ToTable("Explorations", (string)null);
                });

            modelBuilder.Entity("trailAPI.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1000L);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("username");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.HasKey("UserID");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("trailAPI.Models.Exploration", b =>
                {
                    b.HasOne("trailAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
