using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trailAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddExplorationTableFunctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Explorations",
                columns: table => new
                {
                    explorationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    trailID = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "'AA' + RIGHT('0000' + CAST(NEXT VALUE FOR dbo.TrailIDSequence AS VARCHAR(4)), 4)"),
                    completionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    completionStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Explorations", x => x.explorationID);
                    table.ForeignKey(
                        name: "FK_Explorations_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Explorations_userID",
                table: "Explorations",
                column: "userID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Explorations");
        }
    }
}
