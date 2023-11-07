using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace BotWars.Migrations
{
    /// <inheritdoc />
    public partial class dbroz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    BotFile = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.Id);
                    table.UniqueConstraint("AK_Bots_GameId", x => x.GameId);
                    table.UniqueConstraint("AK_Bots_PlayerId", x => x.PlayerId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RockPaperScissors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlayerOneName = table.Column<string>(type: "longtext", nullable: false),
                    PlayerTwoName = table.Column<string>(type: "longtext", nullable: false),
                    SymbolPlayerOne = table.Column<int>(type: "int", nullable: false),
                    SymbolPlayerTwo = table.Column<int>(type: "int", nullable: false),
                    Winner = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RockPaperScissors", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    NumbersOfPlayer = table.Column<int>(type: "int", nullable: false),
                    LastModification = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GameFile = table.Column<string>(type: "longtext", nullable: false),
                    GameInstructions = table.Column<string>(type: "longtext", nullable: false),
                    InterfaceDefinition = table.Column<string>(type: "longtext", nullable: false),
                    IsAvaiableForPlay = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Bots_Id",
                        column: x => x.Id,
                        principalTable: "Bots",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    PlayersLimi = table.Column<int>(type: "int", nullable: false),
                    TournamentsDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WasPlayedOut = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Contrains = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArchivedMatches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    TournamentsId = table.Column<long>(type: "bigint", nullable: false),
                    Played = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Match = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivedMatches_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArchivedMatches_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArchivedMatchPlayers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    TournamentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedMatchPlayers", x => x.Id);
                    table.UniqueConstraint("AK_ArchivedMatchPlayers_PlayerId", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_ArchivedMatchPlayers_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", nullable: true),
                    login = table.Column<string>(type: "longtext", nullable: false),
                    ArchivedMatchPlayersPlayerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_ArchivedMatchPlayers_ArchivedMatchPlayersPlayerId",
                        column: x => x.ArchivedMatchPlayersPlayerId,
                        principalTable: "ArchivedMatchPlayers",
                        principalColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_Players_Bots_Id",
                        column: x => x.Id,
                        principalTable: "Bots",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedMatches_GameId",
                table: "ArchivedMatches",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedMatches_TournamentsId",
                table: "ArchivedMatches",
                column: "TournamentsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedMatchPlayers_TournamentId",
                table: "ArchivedMatchPlayers",
                column: "TournamentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_ArchivedMatchPlayersPlayerId",
                table: "Players",
                column: "ArchivedMatchPlayersPlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_email",
                table: "Players",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_GameId",
                table: "Tournaments",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivedMatches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "RockPaperScissors");

            migrationBuilder.DropTable(
                name: "ArchivedMatchPlayers");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Bots");
        }
    }
}
