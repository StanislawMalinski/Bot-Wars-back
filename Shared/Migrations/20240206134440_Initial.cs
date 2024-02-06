using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_Task",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ScheduledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Refid = table.Column<long>(type: "bigint", nullable: false),
                    ParentTaskId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Task__Task_ParentTaskId",
                        column: x => x.ParentTaskId,
                        principalTable: "_Task",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AchievementType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                });

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
                name: "NotificationOutboxes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    NotificationValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationOutboxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AchievementThresholds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Threshold = table.Column<long>(type: "bigint", nullable: false),
                    AchievementTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementThresholds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AchievementThresholds_AchievementType_AchievementTypeId",
                        column: x => x.AchievementTypeId,
                        principalTable: "AchievementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    isBanned = table.Column<bool>(type: "bit", nullable: false),
                    Points = table.Column<long>(type: "bigint", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
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
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AchievementRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<long>(type: "bigint", nullable: false),
                    AchievementTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AchievementRecord_AchievementType_AchievementTypeId",
                        column: x => x.AchievementTypeId,
                        principalTable: "AchievementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementRecord_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
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
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    IsDarkTheme = table.Column<bool>(type: "bit", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    MatchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "AchievementType",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1L, "You need to play this amount of games" },
                    { 2L, "You need to upload this amount of bots" },
                    { 3L, "You need to win this amount of games" },
                    { 4L, "You need to win this amount of tournaments" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "GameFile", "GameInstructions", "InterfaceDefinition", "IsAvailableForPlay", "LastModification", "NumbersOfPlayer" },
                values: new object[,]
                {
                    { 1L, "Quake III Arena", "Eliminate the enemy players in fast-paced multiplayer battles.", "First-Person Shooter (FPS)", true, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7329), 10 },
                    { 2L, "The Legend of Zelda: Breath of the Wild", "Embark on an epic adventure to defeat the Calamity Ganon and save Hyrule.", "Action-Adventure", true, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7380), 1 },
                    { 3L, "FIFA 22", "Experience realistic football simulation with updated teams and gameplay.", "Sports Simulation", true, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7383), 2 },
                    { 4L, "Among Us", "Work together to complete tasks while identifying the impostors among the crew.", "Social Deduction", true, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7385), 7 },
                    { 5L, "Minecraft", "Build and explore a blocky world, mine resources, and survive.", "Sandbox", false, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7388), 16 },
                    { 6L, "Cyberpunk 2077", "Navigate the futuristic open world of Night City as the mercenary V.", "Action RPG", true, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7391), 1 },
                    { 7L, "Rocket League", "Play soccer with rocket-powered cars in this unique sports game.", "Vehicular Soccer", true, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7393), 14 },
                    { 8L, "Call of Duty: Warzone", "Engage in intense battle royale action in the Call of Duty universe.", "First-Person Shooter (Battle Royale)", false, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7396), 8 },
                    { 9L, "Animal Crossing: New Horizons", "Create and customize your own island paradise in a relaxing simulation.", "Life Simulation", true, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7398), 5 },
                    { 10L, "Dota 2", "Compete in strategic team-based battles in this multiplayer online battle arena (MOBA).", "MOBA", true, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7401), 10 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AchievementThresholds",
                columns: new[] { "Id", "AchievementTypeId", "Threshold" },
                values: new object[,]
                {
                    { 1L, 1L, 10L },
                    { 2L, 2L, 4L },
                    { 3L, 3L, 5L },
                    { 4L, 4L, 1L },
                    { 5L, 1L, 20L },
                    { 6L, 2L, 8L },
                    { 7L, 3L, 10L },
                    { 8L, 4L, 5L }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Email", "HashedPassword", "Login", "Points", "RoleId", "isBanned" },
                values: new object[,]
                {
                    { 1L, "john.doe@example.com", "aasdsdas", "john_doe", 100L, 1, false },
                    { 2L, "jane.smith@example.com", "sdfgdfg", "jane_smith", 150L, 1, false },
                    { 3L, "alex.jones@example.com", "hjklhjk", "alex_jones", 200L, 1, false },
                    { 4L, "emily.white@example.com", "qwertyui", "emily_white", 75L, 1, false },
                    { 5L, "sam.wilson@example.com", "zxcvbnm", "sam_wilson", 120L, 1, false },
                    { 6L, "olivia.brown@example.com", "poiuytre", "olivia_brown", 180L, 1, false },
                    { 7L, "david.miller@example.com", "lkjhgfds", "david_miller", 90L, 1, false },
                    { 8L, "emma.jenkins@example.com", "mnbvcxz", "emma_jenkins", 160L, 1, false },
                    { 9L, "ryan.clark@example.com", "asdfghjk", "ryan_clark", 110L, 1, true },
                    { 10L, "sara.taylor@example.com", "qazwsxed", "sara_taylor", 130L, 2, false }
                });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "Constraints", "Description", "GameId", "Image", "PlayersLimit", "PostedDate", "TournamentTitle", "TournamentsDate", "WasPlayedOut" },
                values: new object[,]
                {
                    { 1L, "Participants must have a minimum skill level of intermediate.", "Compete in the ultimate Quake III Arena tournament and prove your skills in fast-paced multiplayer battles.", 1L, "quakethreearena.jpg", 0, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7432), "Quake III Arena Championship", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 2L, "Participants must complete the game on a specific difficulty level.", "Embark on a quest to become the master of The Legend of Zelda: Breath of the Wild. Solve puzzles and defeat foes to claim victory.", 2L, "zeldabreathofthewild.jpg", 0, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7439), "Zelda Master Cup", new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 3L, "Teams must consist of real-world players.", "Experience the thrill of virtual football in the FIFA 22 World Cup. Compete with players from around the globe for the championship.", 3L, "fifa22worldcup.jpg", 0, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7442), "FIFA 22 World Cup", new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 4L, "Players must use voice communication during the game.", "Test your deception skills in the Among Us Infiltration Challenge. Work as a crew member or impostor to secure victory.", 4L, "amongusinfiltration.jpg", 0, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7445), "Among Us Infiltration Challenge", new DateTime(2022, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 5L, "Builds must adhere to a specific theme.", "Showcase your creative building skills in the Minecraft Building Showcase. Construct impressive structures and compete for recognition.", 5L, "minecraftbuildingshowcase.jpg", 0, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7447), "Minecraft Building Showcase", new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 6L, "Participants must customize their character's appearance.", "Immerse yourself in the cyberpunk world of Night City. Compete in cyberwarfare challenges and emerge as the ultimate netrunner.", 6L, "cyberpunk2077challenge.jpg", 0, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7450), "Cyberpunk 2077 Cyberwarfare Challenge", new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 7L, "Teams must consist of three players.", "Take part in high-flying, rocket-powered soccer action. Compete in the Rocket League Championship and score goals to victory.", 7L, "rocketleaguechampionship.jpg", 0, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7453), "Rocket League Championship", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 8L, "Players must adhere to the battle royale ruleset.", "Join the intense battle royale action in Call of Duty: Warzone. Compete against other squads to be the last team standing.", 8L, "callofdutywarzone.jpg", 0, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7456), "Call of Duty: Warzone Battle Royale", new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 9L, "Islands must be designed within a specific theme.", "Create the most charming and unique island paradise in the Animal Crossing Island Showcase. Display your creativity and win accolades.", 9L, "animalcrossingislandshowcase.jpg", 0, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7458), "Animal Crossing Island Showcase", new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 10L, "Teams must adhere to the standard Dota 2 competitive rules.", "Enter the world of strategic battles in the Dota 2 Clash of Titans. Assemble your team, choose your heroes, and conquer the opposition.", 10L, "dota2clashoftitans.jpg", 0, new DateTime(2024, 2, 6, 14, 44, 39, 732, DateTimeKind.Local).AddTicks(7482), "Dota 2 Clash of Titans", new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false }
                });

            migrationBuilder.InsertData(
                table: "AchievementRecord",
                columns: new[] { "Id", "AchievementTypeId", "PlayerId", "Value" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, 10L },
                    { 2L, 2L, 1L, 15L },
                    { 3L, 1L, 2L, 10L },
                    { 4L, 2L, 2L, 15L }
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
                name: "IX__Task_ParentTaskId",
                table: "_Task",
                column: "ParentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementRecord_AchievementTypeId",
                table: "AchievementRecord",
                column: "AchievementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementRecord_PlayerId",
                table: "AchievementRecord",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementThresholds_AchievementTypeId",
                table: "AchievementThresholds",
                column: "AchievementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_GameId",
                table: "Bots",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_PlayerId",
                table: "Bots",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_GameId",
                table: "Matches",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentsId",
                table: "Matches",
                column: "TournamentsId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_MatchId",
                table: "MatchPlayers",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_PlayerId",
                table: "MatchPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_RoleId",
                table: "Players",
                column: "RoleId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_PlayerId",
                table: "UserSettings",
                column: "PlayerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_Task");

            migrationBuilder.DropTable(
                name: "AchievementRecord");

            migrationBuilder.DropTable(
                name: "AchievementThresholds");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "MatchPlayers");

            migrationBuilder.DropTable(
                name: "NotificationOutboxes");

            migrationBuilder.DropTable(
                name: "PointHistories");

            migrationBuilder.DropTable(
                name: "TournamentReferences");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "AchievementType");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Bots");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
