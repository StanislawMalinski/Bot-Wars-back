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
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ScheduledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    OperatingOn = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
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
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Validation = table.Column<int>(type: "int", nullable: false),
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
                name: "NotificationOutboxes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    NotificationValue = table.Column<int>(type: "int", nullable: false),
                    PLayerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationOutboxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationOutboxes_Players_PLayerId",
                        column: x => x.PLayerId,
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
                    Status = table.Column<int>(type: "int", nullable: false),
                    RankingType = table.Column<int>(type: "int", nullable: false),
                    Constraints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tournaments_Players_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Players",
                        principalColumn: "Id");
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
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    TournamentsId = table.Column<long>(type: "bigint", nullable: false),
                    Played = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Winner = table.Column<long>(type: "bigint", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
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
                name: "MatchPlayers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BotId = table.Column<long>(type: "bigint", nullable: false),
                    MatchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id");
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
                    { 1L, "Quake III Arena", "Eliminate the enemy players in fast-paced multiplayer battles.", "First-Person Shooter (FPS)", true, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8510), 10 },
                    { 2L, "The Legend of Zelda: Breath of the Wild", "Embark on an epic adventure to defeat the Calamity Ganon and save Hyrule.", "Action-Adventure", true, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8517), 1 },
                    { 3L, "FIFA 22", "Experience realistic football simulation with updated teams and gameplay.", "Sports Simulation", true, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8520), 2 },
                    { 4L, "Among Us", "Work together to complete tasks while identifying the impostors among the crew.", "Social Deduction", true, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8522), 7 },
                    { 5L, "Minecraft", "Build and explore a blocky world, mine resources, and survive.", "Sandbox", false, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8525), 16 },
                    { 6L, "Cyberpunk 2077", "Navigate the futuristic open world of Night City as the mercenary V.", "Action RPG", true, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8528), 1 },
                    { 7L, "Rocket League", "Play soccer with rocket-powered cars in this unique sports game.", "Vehicular Soccer", true, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8530), 14 },
                    { 8L, "Call of Duty: Warzone", "Engage in intense battle royale action in the Call of Duty universe.", "First-Person Shooter (Battle Royale)", false, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8533), 8 },
                    { 9L, "Animal Crossing: New Horizons", "Create and customize your own island paradise in a relaxing simulation.", "Life Simulation", true, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8535), 5 },
                    { 10L, "Dota 2", "Compete in strategic team-based battles in this multiplayer online battle arena (MOBA).", "MOBA", true, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8538), 10 }
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
                    { 8L, 4L, 5L },
                    { 9L, 1L, 5L }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Email", "HashedPassword", "LastLogin", "Login", "Points", "Registered", "RoleId", "isBanned" },
                values: new object[,]
                {
                    { 1L, "john.doe@example.com", "aasdsdas", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john_doe", 1000L, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8337), 1, false },
                    { 2L, "jane.smith@example.com", "sdfgdfg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane_smith", 1000L, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8386), 1, false },
                    { 3L, "alex.jones@example.com", "hjklhjk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alex_jones", 1000L, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8443), 1, false },
                    { 4L, "emily.white@example.com", "qwertyui", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily_white", 1000L, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8446), 1, false },
                    { 5L, "sam.wilson@example.com", "zxcvbnm", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sam_wilson", 1000L, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8449), 1, false },
                    { 6L, "olivia.brown@example.com", "poiuytre", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "olivia_brown", 1000L, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8454), 1, false },
                    { 7L, "david.miller@example.com", "lkjhgfds", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "david_miller", 1000L, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8456), 1, false },
                    { 8L, "emma.jenkins@example.com", "mnbvcxz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emma_jenkins", 1000L, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8458), 1, false },
                    { 9L, "ryan.clark@example.com", "asdfghjk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ryan_clark", 1000L, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8461), 1, true },
                    { 10L, "sara.taylor@example.com", "qazwsxed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara_taylor", 1000L, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8464), 2, false }
                });

            migrationBuilder.InsertData(
                table: "AchievementRecord",
                columns: new[] { "Id", "AchievementTypeId", "PlayerId", "Value" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, 20L },
                    { 2L, 2L, 1L, 15L },
                    { 3L, 1L, 2L, 10L },
                    { 4L, 2L, 2L, 15L }
                });

            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "BotFile", "GameId", "PlayerId", "Validation" },
                values: new object[,]
                {
                    { 1L, "quake3_bot_1", 1L, 1L, 0 },
                    { 2L, "quake3_bot_2", 1L, 2L, 0 },
                    { 3L, "zelda_bot_1", 2L, 3L, 0 },
                    { 4L, "zelda_bot_2", 2L, 4L, 0 },
                    { 5L, "fifa22_bot_1", 3L, 5L, 0 },
                    { 6L, "fifa22_bot_2", 3L, 6L, 0 },
                    { 7L, "amongus_bot_1", 4L, 7L, 0 },
                    { 8L, "amongus_bot_2", 4L, 8L, 0 },
                    { 9L, "minecraft_bot_1", 5L, 9L, 0 },
                    { 10L, "minecraft_bot_2", 5L, 10L, 0 }
                });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "Constraints", "CreatorId", "Description", "GameId", "Image", "PlayersLimit", "PostedDate", "RankingType", "Status", "TournamentTitle", "TournamentsDate" },
                values: new object[,]
                {
                    { 1L, "Participants must have a minimum skill level of intermediate.", 1L, "Compete in the ultimate Quake III Arena tournament and prove your skills in fast-paced multiplayer battles.", 1L, "quakethreearena.jpg", 0, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8575), 0, 2, "Quake III Arena Championship", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, "Participants must complete the game on a specific difficulty level.", 2L, "Embark on a quest to become the master of The Legend of Zelda: Breath of the Wild. Solve puzzles and defeat foes to claim victory.", 2L, "zeldabreathofthewild.jpg", 0, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8583), 0, 2, "Zelda Master Cup", new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, "Teams must consist of real-world players.", 3L, "Experience the thrill of virtual football in the FIFA 22 World Cup. Compete with players from around the globe for the championship.", 3L, "fifa22worldcup.jpg", 0, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8586), 0, 2, "FIFA 22 World Cup", new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, "Players must use voice communication during the game.", 1L, "Test your deception skills in the Among Us Infiltration Challenge. Work as a crew member or impostor to secure victory.", 4L, "amongusinfiltration.jpg", 0, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8589), 0, 2, "Among Us Infiltration Challenge", new DateTime(2022, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5L, "Builds must adhere to a specific theme.", 1L, "Showcase your creative building skills in the Minecraft Building Showcase. Construct impressive structures and compete for recognition.", 5L, "minecraftbuildingshowcase.jpg", 0, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8593), 0, 2, "Minecraft Building Showcase", new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6L, "Participants must customize their character's appearance.", 3L, "Immerse yourself in the cyberpunk world of Night City. Compete in cyberwarfare challenges and emerge as the ultimate netrunner.", 6L, "cyberpunk2077challenge.jpg", 0, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8597), 0, 2, "Cyberpunk 2077 Cyberwarfare Challenge", new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7L, "Teams must consist of three players.", 2L, "Take part in high-flying, rocket-powered soccer action. Compete in the Rocket League Championship and score goals to victory.", 7L, "rocketleaguechampionship.jpg", 0, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8600), 0, 2, "Rocket League Championship", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8L, "Players must adhere to the battle royale ruleset.", 2L, "Join the intense battle royale action in Call of Duty: Warzone. Compete against other squads to be the last team standing.", 8L, "callofdutywarzone.jpg", 0, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8602), 0, 2, "Call of Duty: Warzone Battle Royale", new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9L, "Islands must be designed within a specific theme.", 3L, "Create the most charming and unique island paradise in the Animal Crossing Island Showcase. Display your creativity and win accolades.", 9L, "animalcrossingislandshowcase.jpg", 0, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8605), 0, 2, "Animal Crossing Island Showcase", new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10L, "Teams must adhere to the standard Dota 2 competitive rules.", 1L, "Enter the world of strategic battles in the Dota 2 Clash of Titans. Assemble your team, choose your heroes, and conquer the opposition.", 10L, "dota2clashoftitans.jpg", 0, new DateTime(2024, 3, 13, 10, 55, 11, 309, DateTimeKind.Local).AddTicks(8609), 0, 2, "Dota 2 Clash of Titans", new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                    { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10L, 5L },
                    { 6L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L, 1L },
                    { 7L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6L, 1L },
                    { 8L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7L, 1L },
                    { 9L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8L, 1L },
                    { 10L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9L, 1L }
                });

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
                name: "IX_MatchPlayers_BotId",
                table: "MatchPlayers",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_MatchId",
                table: "MatchPlayers",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationOutboxes_PLayerId",
                table: "NotificationOutboxes",
                column: "PLayerId");

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
                name: "IX_Tournaments_CreatorId",
                table: "Tournaments",
                column: "CreatorId");

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
                name: "Tasks");

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
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
