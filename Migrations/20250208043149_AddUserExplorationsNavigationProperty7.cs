using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trailAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserExplorationsNavigationProperty7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Explorations_Users_userID",
                table: "Explorations");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "Explorations",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "explorationID",
                table: "Explorations",
                newName: "ExplorationID");

            migrationBuilder.RenameIndex(
                name: "IX_Explorations_userID",
                table: "Explorations",
                newName: "IX_Explorations_UserID");

            migrationBuilder.CreateTable(
                name: "UsersWithExplorations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "2000, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersWithExplorations", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Explorations_UsersWithExplorations_UserID",
                table: "Explorations",
                column: "UserID",
                principalTable: "UsersWithExplorations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Explorations_UsersWithExplorations_UserID",
                table: "Explorations");

            migrationBuilder.DropTable(
                name: "UsersWithExplorations");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Explorations",
                newName: "userID");

            migrationBuilder.RenameColumn(
                name: "ExplorationID",
                table: "Explorations",
                newName: "explorationID");

            migrationBuilder.RenameIndex(
                name: "IX_Explorations_UserID",
                table: "Explorations",
                newName: "IX_Explorations_userID");

            migrationBuilder.AddForeignKey(
                name: "FK_Explorations_Users_userID",
                table: "Explorations",
                column: "userID",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
