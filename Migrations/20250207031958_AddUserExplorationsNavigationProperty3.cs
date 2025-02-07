using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trailAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserExplorationsNavigationProperty3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "trailID",
                table: "Explorations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Explorations_trailID",
                table: "Explorations",
                column: "trailID");

            migrationBuilder.AddForeignKey(
                name: "FK_Explorations_Trail_Information_trailID",
                table: "Explorations",
                column: "trailID",
                principalTable: "Trail_Information",
                principalColumn: "trailID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Explorations_Trail_Information_trailID",
                table: "Explorations");

            migrationBuilder.DropIndex(
                name: "IX_Explorations_trailID",
                table: "Explorations");

            migrationBuilder.AlterColumn<string>(
                name: "trailID",
                table: "Explorations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
