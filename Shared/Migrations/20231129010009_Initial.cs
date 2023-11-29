using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumbersOfPlayer = table.Column<int>(type: "int", nullable: false),
                    LastModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterfaceDefinition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvaiableForPlay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    login = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentTitles = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    PlayersLimit = table.Column<int>(type: "int", nullable: false),
                    TournamentsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WasPlayedOut = table.Column<bool>(type: "bit", nullable: false),
                    Contrains = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    BotFile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bots_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bots_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchivedMatches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    TournamentsId = table.Column<long>(type: "bigint", nullable: false),
                    Played = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Match = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivedMatches_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchivedMatches_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TournamentReference",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tournamentId = table.Column<long>(type: "bigint", nullable: false),
                    bodId = table.Column<long>(type: "bigint", nullable: false),
                    BotId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentReference_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TournamentReference_Tournaments_tournamentId",
                        column: x => x.tournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchivedMatchPlayers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    TournamentId = table.Column<long>(type: "bigint", nullable: false),
                    MatchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedMatchPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivedMatchPlayers_ArchivedMatches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "ArchivedMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArchivedMatchPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArchivedMatchPlayers_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedMatches_GameId",
                table: "ArchivedMatches",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedMatches_TournamentsId",
                table: "ArchivedMatches",
                column: "TournamentsId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedMatchPlayers_MatchId",
                table: "ArchivedMatchPlayers",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedMatchPlayers_PlayerId",
                table: "ArchivedMatchPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedMatchPlayers_TournamentId",
                table: "ArchivedMatchPlayers",
                column: "TournamentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bots_GameId",
                table: "Bots",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_PlayerId",
                table: "Bots",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentReference_BotId",
                table: "TournamentReference",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentReference_tournamentId",
                table: "TournamentReference",
                column: "tournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_GameId",
                table: "Tournaments",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivedMatchPlayers");

            migrationBuilder.DropTable(
                name: "TournamentReference");

            migrationBuilder.DropTable(
                name: "ArchivedMatches");

            migrationBuilder.DropTable(
                name: "Bots");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
