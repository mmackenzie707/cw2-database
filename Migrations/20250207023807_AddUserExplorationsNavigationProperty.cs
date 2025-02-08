using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trailAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserExplorationsNavigationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Users",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Users",
                newName: "firstName");

            migrationBuilder.AlterColumn<string>(
                name: "trailID",
                table: "Explorations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "'AA' + RIGHT('0000' + CAST(NEXT VALUE FOR dbo.TrailIDSequence AS VARCHAR(4)), 4)");

            migrationBuilder.CreateTable(
                name: "Trail_Information",
                columns: table => new
                {
                    trailID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrailName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trail_Information", x => x.trailID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trail_Information");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "username");

            migrationBuilder.AlterColumn<string>(
                name: "trailID",
                table: "Explorations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "'AA' + RIGHT('0000' + CAST(NEXT VALUE FOR dbo.TrailIDSequence AS VARCHAR(4)), 4)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
