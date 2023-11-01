using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotWars.Migrations
{
    /// <inheritdoc />
    public partial class WinnerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Winner",
                schema: "Games",
                table: "RockPaperScissors",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Winner",
                schema: "Games",
                table: "RockPaperScissors");
        }
    }
}
