using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trailAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstAndLastNameToUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "Users");
        }
    }
}
