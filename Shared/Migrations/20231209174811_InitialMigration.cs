using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                    GameFile = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    GameInstructions = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    InterfaceDefinition = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    IsAvailableForPlay = table.Column<bool>(type: "bit", nullable: false)
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
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Points = table.Column<long>(type: "bigint", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    TournamentTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    PlayersLimit = table.Column<int>(type: "int", nullable: false),
                    TournamentsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WasPlayedOut = table.Column<bool>(type: "bit", nullable: false),
                    Constraints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    BotFile = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
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
                name: "PointHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Loss = table.Column<long>(type: "bigint", nullable: false),
                    Gain = table.Column<long>(type: "bigint", nullable: false),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointHistories_Players_PlayerId",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArchivedMatches_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TournamentReferences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tournamentId = table.Column<long>(type: "bigint", nullable: false),
                    botId = table.Column<long>(type: "bigint", nullable: false),
                    LastModification = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentReferences_Bots_botId",
                        column: x => x.botId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentReferences_Tournaments_tournamentId",
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
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "GameFile", "GameInstructions", "InterfaceDefinition", "IsAvailableForPlay", "LastModification", "NumbersOfPlayer" },
                values: new object[,]
                {
                    { 1L, "Quake III Arena", "Eliminate the enemy players in fast-paced multiplayer battles.", "First-Person Shooter (FPS)", true, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8157), 10 },
                    { 2L, "The Legend of Zelda: Breath of the Wild", "Embark on an epic adventure to defeat the Calamity Ganon and save Hyrule.", "Action-Adventure", true, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8221), 1 },
                    { 3L, "FIFA 22", "Experience realistic football simulation with updated teams and gameplay.", "Sports Simulation", true, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8224), 2 },
                    { 4L, "Among Us", "Work together to complete tasks while identifying the impostors among the crew.", "Social Deduction", true, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8227), 7 },
                    { 5L, "Minecraft", "Build and explore a blocky world, mine resources, and survive.", "Sandbox", false, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8229), 16 },
                    { 6L, "Cyberpunk 2077", "Navigate the futuristic open world of Night City as the mercenary V.", "Action RPG", true, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8233), 1 },
                    { 7L, "Rocket League", "Play soccer with rocket-powered cars in this unique sports game.", "Vehicular Soccer", true, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8236), 14 },
                    { 8L, "Call of Duty: Warzone", "Engage in intense battle royale action in the Call of Duty universe.", "First-Person Shooter (Battle Royale)", false, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8238), 8 },
                    { 9L, "Animal Crossing: New Horizons", "Create and customize your own island paradise in a relaxing simulation.", "Life Simulation", true, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8241), 5 },
                    { 10L, "Dota 2", "Compete in strategic team-based battles in this multiplayer online battle arena (MOBA).", "MOBA", true, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8244), 10 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Email", "HashedPassword", "Login", "Points" },
                values: new object[,]
                {
                    { 1L, "john.doe@example.com", "aasdsdas", "john_doe", 100L },
                    { 2L, "jane.smith@example.com", "sdfgdfg", "jane_smith", 150L },
                    { 3L, "alex.jones@example.com", "hjklhjk", "alex_jones", 200L },
                    { 4L, "emily.white@example.com", "qwertyui", "emily_white", 75L },
                    { 5L, "sam.wilson@example.com", "zxcvbnm", "sam_wilson", 120L },
                    { 6L, "olivia.brown@example.com", "poiuytre", "olivia_brown", 180L },
                    { 7L, "david.miller@example.com", "lkjhgfds", "david_miller", 90L },
                    { 8L, "emma.jenkins@example.com", "mnbvcxz", "emma_jenkins", 160L },
                    { 9L, "ryan.clark@example.com", "asdfghjk", "ryan_clark", 110L },
                    { 10L, "sara.taylor@example.com", "qazwsxed", "sara_taylor", 130L }
                });

            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "BotFile", "GameId", "PlayerId" },
                values: new object[,]
                {
                    { 1L, "quake3_bot_1", 1L, 1L },
                    { 2L, "quake3_bot_2", 1L, 2L },
                    { 3L, "zelda_bot_1", 2L, 3L },
                    { 4L, "zelda_bot_2", 2L, 4L },
                    { 5L, "fifa22_bot_1", 3L, 5L },
                    { 6L, "fifa22_bot_2", 3L, 6L },
                    { 7L, "amongus_bot_1", 4L, 7L },
                    { 8L, "amongus_bot_2", 4L, 8L },
                    { 9L, "minecraft_bot_1", 5L, 9L },
                    { 10L, "minecraft_bot_2", 5L, 10L }
                });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "Constraints", "Description", "GameId", "Image", "PlayersLimit", "PostedDate", "TournamentTitle", "TournamentsDate", "WasPlayedOut" },
                values: new object[,]
                {
                    { 1L, "Participants must have a minimum skill level of intermediate.", "Compete in the ultimate Quake III Arena tournament and prove your skills in fast-paced multiplayer battles.", 1L, "quakethreearena.jpg", 0, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9603), "Quake III Arena Championship", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 2L, "Participants must complete the game on a specific difficulty level.", "Embark on a quest to become the master of The Legend of Zelda: Breath of the Wild. Solve puzzles and defeat foes to claim victory.", 2L, "zeldabreathofthewild.jpg", 0, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9658), "Zelda Master Cup", new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 3L, "Teams must consist of real-world players.", "Experience the thrill of virtual football in the FIFA 22 World Cup. Compete with players from around the globe for the championship.", 3L, "fifa22worldcup.jpg", 0, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9671), "FIFA 22 World Cup", new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 4L, "Players must use voice communication during the game.", "Test your deception skills in the Among Us Infiltration Challenge. Work as a crew member or impostor to secure victory.", 4L, "amongusinfiltration.jpg", 0, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9682), "Among Us Infiltration Challenge", new DateTime(2022, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 5L, "Builds must adhere to a specific theme.", "Showcase your creative building skills in the Minecraft Building Showcase. Construct impressive structures and compete for recognition.", 5L, "minecraftbuildingshowcase.jpg", 0, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9693), "Minecraft Building Showcase", new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 6L, "Participants must customize their character's appearance.", "Immerse yourself in the cyberpunk world of Night City. Compete in cyberwarfare challenges and emerge as the ultimate netrunner.", 6L, "cyberpunk2077challenge.jpg", 0, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9711), "Cyberpunk 2077 Cyberwarfare Challenge", new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 7L, "Teams must consist of three players.", "Take part in high-flying, rocket-powered soccer action. Compete in the Rocket League Championship and score goals to victory.", 7L, "rocketleaguechampionship.jpg", 0, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9724), "Rocket League Championship", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 8L, "Players must adhere to the battle royale ruleset.", "Join the intense battle royale action in Call of Duty: Warzone. Compete against other squads to be the last team standing.", 8L, "callofdutywarzone.jpg", 0, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9729), "Call of Duty: Warzone Battle Royale", new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 9L, "Islands must be designed within a specific theme.", "Create the most charming and unique island paradise in the Animal Crossing Island Showcase. Display your creativity and win accolades.", 9L, "animalcrossingislandshowcase.jpg", 0, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9734), "Animal Crossing Island Showcase", new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 10L, "Teams must adhere to the standard Dota 2 competitive rules.", "Enter the world of strategic battles in the Dota 2 Clash of Titans. Assemble your team, choose your heroes, and conquer the opposition.", 10L, "dota2clashoftitans.jpg", 0, new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9747), "Dota 2 Clash of Titans", new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false }
                });

            migrationBuilder.InsertData(
                table: "TournamentReferences",
                columns: new[] { "Id", "LastModification", "botId", "tournamentId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 1L },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, 1L },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, 2L },
                    { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, 2L },
                    { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10L, 5L }
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
                name: "IX_Bots_GameId",
                table: "Bots",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_PlayerId",
                table: "Bots",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PointHistories_PlayerId",
                table: "PointHistories",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentReferences_botId",
                table: "TournamentReferences",
                column: "botId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentReferences_tournamentId",
                table: "TournamentReferences",
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
                name: "PointHistories");

            migrationBuilder.DropTable(
                name: "TournamentReferences");

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
