using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trailAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserExplorationsNavigationProperty4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrailName",
                table: "Trail_Information",
                newName: "trailName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Trail_Information",
                newName: "trailLocation");

            migrationBuilder.AddColumn<string>(
                name: "trailDescription",
                table: "Trail_Information",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "trailDescription",
                table: "Trail_Information");

            migrationBuilder.RenameColumn(
                name: "trailName",
                table: "Trail_Information",
                newName: "TrailName");

            migrationBuilder.RenameColumn(
                name: "trailLocation",
                table: "Trail_Information",
                newName: "Description");
        }
    }
}
