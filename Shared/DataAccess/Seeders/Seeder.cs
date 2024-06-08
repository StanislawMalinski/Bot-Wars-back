using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.Seeders;

public class Seeder
{
    private const int workFactor = 12;

    public static IEnumerable<Player> GeneratePlayers()
    {
        return new List<Player>
        {
            new()
            {
                Id = 1, Email = "john.doe@example.com", Login = "john_doe", Points = 1000,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234", workFactor), Registered = DateTime.Now,
                isBanned = false, RoleId = 1
            },
            new()
            {
                Id = 2, Email = "jane.smith@example.com", Login = "jane_smith", Points = 1000,
                Registered = DateTime.Now,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234", workFactor), isBanned = false, RoleId = 1
            },
            new()
            {
                Id = 3, Email = "alex.jones@example.com", Login = "alex_jones", Points = 1000,
                Registered = DateTime.Now,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234", workFactor), isBanned = false, RoleId = 1
            },
            new()
            {
                Id = 4, Email = "emily.white@example.com", Login = "emily_white", Points = 1000,
                Registered = DateTime.Now,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234", workFactor), isBanned = false, RoleId = 1
            },
            new()
            {
                Id = 5, Email = "sam.wilson@example.com", Login = "sam_wilson", Points = 1000,
                Registered = DateTime.Now,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234", workFactor), isBanned = false, RoleId = 1
            },
            new()
            {
                Id = 6, Email = "olivia.brown@example.com", Login = "olivia_brown", Points = 1000,
                Registered = DateTime.Now,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234", workFactor), isBanned = false, RoleId = 1
            },
            new()
            {
                Id = 7, Email = "david.miller@example.com", Login = "david_miller", Points = 1000,
                Registered = DateTime.Now,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234", workFactor), isBanned = false, RoleId = 1
            },
            new()
            {
                Id = 8, Email = "emma.jenkins@example.com", Login = "emma_jenkins", Points = 1000,
                Registered = DateTime.Now,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234", workFactor), isBanned = false, RoleId = 1
            },
            new()
            {
                Id = 9, Email = "ryan.clark@example.com", Login = "ryan_clark", Points = 1000,
                Registered = DateTime.Now,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234", workFactor), isBanned = true, RoleId = 1
            },
            new()
            {
                Id = 10, Email = "sara.taylor@example.com", Login = "sara_taylor", Points = 1000,
                Registered = DateTime.Now,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234", workFactor), isBanned = false, RoleId = 2
            }
        };
    }

    public static IEnumerable<Role> GenerateRoles()
    {
        return new List<Role>
        {
            new()
            {
                Id = 1,
                Name = "User"
            },
            new()
            {
                Id = 2,
                Name = "Admin"
            }
        };
    }

    public static IEnumerable<Game> GenerateGames()
    {
        return new List<Game>
        {
            new()
            {
                Id = 1, NumbersOfPlayer = 10, LastModification = DateTime.Now, GameFile = "QuakeIIIArena",
                GameInstructions = "Eliminate the enemy players in fast-paced multiplayer battles.",
                InterfaceDefinition = "First-Person Shooter (FPS)", IsAvailableForPlay = true, FileId = 5,
                CreatorId = 1, Language = Language.C
            },
            new()
            {
                Id = 2, NumbersOfPlayer = 1, LastModification = DateTime.Now,
                GameFile = "TheLegendofZelda",
                GameInstructions = "Embark on an epic adventure to defeat the Calamity Ganon and save Hyrule.",
                InterfaceDefinition = "Action-Adventure", IsAvailableForPlay = true, FileId = 12, CreatorId = 1,
                Language = Language.PYTHON
            },
            new()
            {
                Id = 3, NumbersOfPlayer = 2, LastModification = DateTime.Now, GameFile = "FIFA 22",
                GameInstructions = "Experience realistic football simulation with updated teams and gameplay.",
                InterfaceDefinition = "Sports Simulation", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 4, NumbersOfPlayer = 7, LastModification = DateTime.Now, GameFile = "Among Us",
                GameInstructions = "Work together to complete tasks while identifying the impostors among the crew.",
                InterfaceDefinition = "Social Deduction", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 5, NumbersOfPlayer = 16, LastModification = DateTime.Now, GameFile = "Minecraft",
                GameInstructions = "Build and explore a blocky world, mine resources, and survive.",
                InterfaceDefinition = "Sandbox", IsAvailableForPlay = false, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 6, NumbersOfPlayer = 1, LastModification = DateTime.Now, GameFile = "Cyberpunk 2077",
                GameInstructions = "Navigate the futuristic open world of Night City as the mercenary V.",
                InterfaceDefinition = "Action RPG", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 7, NumbersOfPlayer = 14, LastModification = DateTime.Now, GameFile = "Rocket League",
                GameInstructions = "Play soccer with rocket-powered cars in this unique sports game.",
                InterfaceDefinition = "Vehicular Soccer", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 8, NumbersOfPlayer = 8, LastModification = DateTime.Now, GameFile = "Call of Duty: Warzone",
                GameInstructions = "Engage in intense battle royale action in the Call of Duty universe.",
                InterfaceDefinition = "First-Person Shooter (Battle Royale)", IsAvailableForPlay = false, FileId = 1,
                CreatorId = 1, Language = Language.C
            },
            new()
            {
                Id = 9, NumbersOfPlayer = 5, LastModification = DateTime.Now,
                GameFile = "Animal Crossing: New Horizons",
                GameInstructions = "Create and customize your own island paradise in a relaxing simulation.",
                InterfaceDefinition = "Life Simulation", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 10, NumbersOfPlayer = 10, LastModification = DateTime.Now, GameFile = "Dota 2",
                GameInstructions =
                    "Compete in strategic team-based battles in this multiplayer online battle arena (MOBA).",
                InterfaceDefinition = "MOBA", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            }
        };
    }

    public static IEnumerable<Tournament> GenerateTournaments()
    {
        return new List<Tournament>
        {
            new()
            {
                Id = 1,
                GameId = 1,
                TournamentTitle = "Quake III Arena Championship",
                Description =
                    "Compete in the ultimate Quake III Arena tournament and prove your skills in fast-paced multiplayer battles.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 1, 20),
                Status = TournamentStatus.PLAYED,
                Constraints = "Participants must have a minimum skill level of intermediate.",
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII="), // You can replace this with the actual image file or URL,
                CreatorId = 1, MemoryLimit = 15000, TimeLimit = 2000
            },
            new()
            {
                Id = 2,
                GameId = 2,
                TournamentTitle = "Zelda Master Cup",
                Description =
                    "Embark on a quest to become the master of The Legend of Zelda: Breath of the Wild. Solve puzzles and defeat foes to claim victory.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 2, 15),
                Status = TournamentStatus.PLAYED,
                Constraints = "Participants must complete the game on a specific difficulty level.",
                CreatorId = 2, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=") // You can replace this with the actual image file or URL
            },
            new()
            {
                Id = 3,
                GameId = 3,
                TournamentTitle = "FIFA 22 World Cup",
                Description =
                    "Experience the thrill of virtual football in the FIFA 22 World Cup. Compete with players from around the globe for the championship.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 3, 10),
                Status = TournamentStatus.PLAYED,
                Constraints = "Teams must consist of real-world players.",
                CreatorId = 3, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=") // You can replace this with the actual image file or URL
            },
            new()
            {
                Id = 4,
                GameId = 4,
                TournamentTitle = "Among Us Infiltration Challenge",
                Description =
                    "Test your deception skills in the Among Us Infiltration Challenge. Work as a crew member or impostor to secure victory.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2022, 4, 5),
                Status = TournamentStatus.PLAYED,
                Constraints = "Players must use voice communication during the game.",
                CreatorId = 1, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 5,
                GameId = 5,
                TournamentTitle = "Minecraft Building Showcase",
                Description =
                    "Showcase your creative building skills in the Minecraft Building Showcase. Construct impressive structures and compete for recognition.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 5, 20),
                Status = TournamentStatus.PLAYED,
                Constraints = "Builds must adhere to a specific theme.",
                CreatorId = 1, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 6,
                GameId = 6,
                TournamentTitle = "Cyberpunk 2077 Cyberwarfare Challenge",
                Description =
                    "Immerse yourself in the cyberpunk world of Night City. Compete in cyberwarfare challenges and emerge as the ultimate netrunner.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2022, 6, 15),
                Status = TournamentStatus.PLAYED,
                Constraints = "Participants must customize their character's appearance.",
                CreatorId = 3, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 7,
                GameId = 7,
                TournamentTitle = "Rocket League Championship",
                Description =
                    "Take part in high-flying, rocket-powered soccer action. Compete in the Rocket League Championship and score goals to victory.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 7, 1),
                Status = TournamentStatus.PLAYED,
                Constraints = "Teams must consist of three players.",
                CreatorId = 2, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 8,
                GameId = 8,
                TournamentTitle = "Call of Duty: Warzone Battle Royale",
                Description =
                    "Join the intense battle royale action in Call of Duty: Warzone. Compete against other squads to be the last team standing.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 8, 10),
                Status = TournamentStatus.PLAYED,
                Constraints = "Players must adhere to the battle royale ruleset.",
                CreatorId = 2, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 9,
                GameId = 9,
                TournamentTitle = "Animal Crossing Island Showcase",
                Description =
                    "Create the most charming and unique island paradise in the Animal Crossing Island Showcase. Display your creativity and win accolades.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 9, 5),
                Status = TournamentStatus.PLAYED,
                Constraints = "Islands must be designed within a specific theme.",
                CreatorId = 3, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 10,
                GameId = 10,
                TournamentTitle = "Dota 2 Clash of Titans",
                Description =
                    "Enter the world of strategic battles in the Dota 2 Clash of Titans. Assemble your team, choose your heroes, and conquer the opposition.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 10, 20),
                Status = TournamentStatus.PLAYED,
                Constraints = "Teams must adhere to the standard Dota 2 competitive rules.",
                CreatorId = 1, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 11,
                GameId = 10,
                TournamentTitle = "Dota 2 Clash of Titans",
                Description =
                    "Enter the world of strategic battles in the Dota 2 Clash of Titans. Assemble your team, choose your heroes, and conquer the opposition.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2025, 10, 20),
                Status = TournamentStatus.SCHEDULED,
                Constraints = "Teams must adhere to the standard Dota 2 competitive rules.",
                CreatorId = 1, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            }
        };
    }

    public static IEnumerable<Bot> GenerateBots()
    {
        return new List<Bot>
        {
            new()
            {
                Id = 1, GameId = 1, PlayerId = 1, BotFile = "quake3_bot_1", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 2, GameId = 1, PlayerId = 2, BotFile = "quake3_bot_2", FileId = 3, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 3, GameId = 1, PlayerId = 3, BotFile = "zelda_bot_1", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 4, GameId = 1, PlayerId = 4, BotFile = "zelda_bot_2", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 5, GameId = 1, PlayerId = 5, BotFile = "fifa22_bot_1", FileId = 4, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 6, GameId = 1, PlayerId = 6, BotFile = "fifa22_bot_2", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 7, GameId = 1, PlayerId = 7, BotFile = "amongus_bot_1", FileId = 3, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 8, GameId = 1, PlayerId = 8, BotFile = "amongus_bot_2", FileId = 4, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 9, GameId = 1, PlayerId = 9, BotFile = "minecra", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 10, GameId = 1, PlayerId = 10, BotFile = "minecr", FileId = 4, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 11, GameId = 2, PlayerId = 2, BotFile = "minecraft_bot_2", FileId = 9, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 12, GameId = 2, PlayerId = 3, BotFile = "minecraft_bot_2", FileId = 10, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 13, GameId = 2, PlayerId = 4, BotFile = "minecraft_bot_2", FileId = 11, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 14, GameId = 2, PlayerId = 5, BotFile = "minecraft_bot_2", FileId = 9, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 15, GameId = 2, PlayerId = 6, BotFile = "minecraft_bot_2", FileId = 10, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 16, GameId = 2, PlayerId = 7, BotFile = "minecraft_bot_2", FileId = 11, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 17, GameId = 2, PlayerId = 8, BotFile = "bot_zero_5.java", FileId = 8, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.Java, Validation = BotStatus.NotValidated
            }
        };
    }

    public static IEnumerable<TournamentReference> GenerateTournamentReferences()
    {
        return new List<TournamentReference>
        {
            new() { Id = 1, tournamentId = 1, botId = 1 },
            new() { Id = 2, tournamentId = 1, botId = 2 },
            new() { Id = 3, tournamentId = 3, botId = 3 },
            new() { Id = 4, tournamentId = 3, botId = 4 },
            new() { Id = 5, tournamentId = 3, botId = 10 },
            new() { Id = 6, tournamentId = 1, botId = 5 },
            new() { Id = 7, tournamentId = 1, botId = 6 },
            new() { Id = 8, tournamentId = 1, botId = 7 },
            new() { Id = 9, tournamentId = 1, botId = 8 },
            new() { Id = 10, tournamentId = 1, botId = 9 },
            new() { Id = 11, tournamentId = 2, botId = 11 },
            new() { Id = 12, tournamentId = 2, botId = 12 },
            new() { Id = 13, tournamentId = 2, botId = 13 },
            new() { Id = 14, tournamentId = 2, botId = 14 },
            new() { Id = 15, tournamentId = 2, botId = 15 },
            new() { Id = 16, tournamentId = 2, botId = 16 },
            new() { Id = 17, tournamentId = 2, botId = 17 }
        };
    }

    public static IEnumerable<AchievementType> GenerateAchievementTypes()
    {
        return new List<AchievementType>
        {
            new() { Id = (int)AchievementsTypes.GamePlayed, Description = "You need to play this amount of games" },
            new() { Id = (int)AchievementsTypes.BotsUploads, Description = "You need to upload this amount of bots" },
            new() { Id = (int)AchievementsTypes.WinGames, Description = "You need to win this amount of games" },
            new()
            {
                Id = (int)AchievementsTypes.TournamentsWon, Description = "You need to win this amount of tournaments"
            },
            new()
            {
                Id = (int)AchievementsTypes.AccountCreated, Description = "Creating Account"
            }
        };
    }

    public static IEnumerable<AchievementThresholds> GenerateAchievementThresholds()
    {
        return new List<AchievementThresholds>
        {
            new() { Id = 1, AchievementTypeId = (int)AchievementsTypes.GamePlayed, Threshold = 10 },
            new() { Id = 2, AchievementTypeId = (int)AchievementsTypes.BotsUploads, Threshold = 4 },
            new() { Id = 3, AchievementTypeId = (int)AchievementsTypes.WinGames, Threshold = 5 },
            new() { Id = 4, AchievementTypeId = (int)AchievementsTypes.TournamentsWon, Threshold = 1 },
            new() { Id = 5, AchievementTypeId = (int)AchievementsTypes.GamePlayed, Threshold = 20 },
            new() { Id = 6, AchievementTypeId = (int)AchievementsTypes.BotsUploads, Threshold = 8 },
            new() { Id = 7, AchievementTypeId = (int)AchievementsTypes.WinGames, Threshold = 10 },
            new() { Id = 8, AchievementTypeId = (int)AchievementsTypes.TournamentsWon, Threshold = 5 },
            new() { Id = 9, AchievementTypeId = (int)AchievementsTypes.GamePlayed, Threshold = 5 },
            new() { Id = 10, AchievementTypeId = (int)AchievementsTypes.AccountCreated, Threshold = 1 }
        };
    }

    public static IEnumerable<AchievementRecord> GenerateAchievementRecords()
    {
        return new List<AchievementRecord>
        {
            new() { Id = 1, AchievementTypeId = 1, PlayerId = 1, Value = 20 },
            new() { Id = 2, AchievementTypeId = 2, PlayerId = 1, Value = 15 },
            new() { Id = 3, AchievementTypeId = 1, PlayerId = 2, Value = 10 },
            new() { Id = 4, AchievementTypeId = 2, PlayerId = 2, Value = 15 },
            new() { Id = 5, AchievementTypeId = 5, PlayerId = 1, Value = 1 },
            new() { Id = 6, AchievementTypeId = 5, PlayerId = 2, Value = 1 },
            new() { Id = 7, AchievementTypeId = 5, PlayerId = 3, Value = 1 },
            new() { Id = 8, AchievementTypeId = 5, PlayerId = 4, Value = 1 },
            new() { Id = 9, AchievementTypeId = 5, PlayerId = 5, Value = 1 },
            new() { Id = 10, AchievementTypeId = 5, PlayerId = 6, Value = 1 },
            new() { Id = 11, AchievementTypeId = 5, PlayerId = 7, Value = 1 },
            new() { Id = 12, AchievementTypeId = 5, PlayerId = 8, Value = 1 },
            new() { Id = 13, AchievementTypeId = 5, PlayerId = 9, Value = 1 },
            new() { Id = 14, AchievementTypeId = 5, PlayerId = 10, Value = 1 }
        };
    }
}