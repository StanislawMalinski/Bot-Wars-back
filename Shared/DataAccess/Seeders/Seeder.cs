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
                Id = 1, NumbersOfPlayer = 8, LastModification = DateTime.Now, GameFile = "Blackjack",
                GameInstructions = "Join us for an exciting Blackjack Bots Tournament! Programmers and enthusiasts from around the world will compete by developing advanced AI bots to play Blackjack. Witness intense, strategic gameplay as these bots go head-to-head, showcasing their skills in probability, strategy, and decision-making. Whether you're a coder or a Blackjack fan, this tournament promises thrilling action and innovative AI in every hand dealt!",
                InterfaceDefinition = "Classical Card Game, Gambling", IsAvailableForPlay = true, FileId = 5,
                CreatorId = 1, Language = Language.C
            },
            new()
            {
                Id = 2, NumbersOfPlayer = 2, LastModification = DateTime.Now,
                GameFile = "Zero",
                GameInstructions = "Reduce your points to Zero by strategically improving your hand and prevent your opponents from doing the same!",
                InterfaceDefinition = "Card Game", IsAvailableForPlay = true, FileId = 12, CreatorId = 1,
                Language = Language.PYTHON
            },
            new()
            {
                Id = 3, NumbersOfPlayer = 2, LastModification = DateTime.Now, GameFile = "Checkers++",
                GameInstructions = "Join us for an exhilarating Checkers Bots Tournament! Talented programmers and AI enthusiasts from across the globe will compete by designing sophisticated bots to play Checkers. Watch as these intelligent bots face off in strategic battles, demonstrating their prowess in planning, tactics, and decision-making. Whether you're a developer or a Checkers enthusiast, this tournament guarantees captivating matches and cutting-edge AI strategies in every move!",
                InterfaceDefinition = "Abstract Strategy, Board Game", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 4, NumbersOfPlayer = 2, LastModification = DateTime.Now, GameFile = "Chess",
                GameInstructions = "Strategize and checkmate your opponent's king.",
                InterfaceDefinition = "Strategy", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 5, NumbersOfPlayer = 4, LastModification = DateTime.Now, GameFile = "Monopoly",
                GameInstructions = "Buy, trade, and manage properties to bankrupt your opponents.",
                InterfaceDefinition = "Economic Strategy", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 6, NumbersOfPlayer = 4, LastModification = DateTime.Now, GameFile = "Scrabble",
                GameInstructions = "Create words on the board with letter tiles to score points.",
                InterfaceDefinition = "Word Game", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 7, NumbersOfPlayer = 2, LastModification = DateTime.Now, GameFile = "Checkers",
                GameInstructions = "Capture all of your opponent's pieces by jumping over them.",
                InterfaceDefinition = "Strategy", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 8, NumbersOfPlayer = 2, LastModification = DateTime.Now, GameFile = "Backgammon",
                GameInstructions = "Move all your pieces off the board before your opponent does.",
                InterfaceDefinition = "Strategy", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 9, NumbersOfPlayer = 4, LastModification = DateTime.Now, GameFile = "Bridge",
                GameInstructions = "Compete in teams to win tricks and score points.",
                InterfaceDefinition = "Card Game", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 10, NumbersOfPlayer = 4, LastModification = DateTime.Now, GameFile = "Clue",
                GameInstructions = "Solve the mystery by deducing the murderer, weapon, and location.",
                InterfaceDefinition = "Deduction", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
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
                TournamentTitle = "Blackjack Bots Showdown",
                Description =
                    "Join us for an exciting Blackjack Bots Tournament! Programmers and enthusiasts from around the world will compete by developing advanced AI bots to play Blackjack. Witness intense, strategic gameplay as these bots go head-to-head, showcasing their skills in probability, strategy, and decision-making. Whether you're a coder or a Blackjack fan, this tournament promises thrilling action and innovative AI in every hand dealt!",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 1, 20),
                Status = TournamentStatus.PLAYED,
                Constraints = "Bots must complete each hand within 5 seconds. Maximum memory usage: 15000 KB.",
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII="),
                CreatorId = 1, MemoryLimit = 15000, TimeLimit = 2000
            },
            new()
            {
                Id = 2,
                GameId = 2,
                TournamentTitle = "Zero Card Game Challenge",
                Description =
                    "Reduce your points to Zero by strategically improving your hand and prevent your opponents from doing the same! Prove your skills in this exciting card game tournament.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 2, 15),
                Status = TournamentStatus.PLAYED,
                Constraints = "Each round must be completed within 10 minutes. Maximum memory usage: 15000 KB.",
                CreatorId = 2, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 3,
                GameId = 3,
                TournamentTitle = "Checkers++ Championship",
                Description =
                    "Join us for an exhilarating Checkers Bots Tournament! Talented programmers and AI enthusiasts from across the globe will compete by designing sophisticated bots to play Checkers. Watch as these intelligent bots face off in strategic battles, demonstrating their prowess in planning, tactics, and decision-making. Whether you're a developer or a Checkers enthusiast, this tournament guarantees captivating matches and cutting-edge AI strategies in every move!",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 3, 10),
                Status = TournamentStatus.PLAYED,
                Constraints = "Each move must be made within 10 seconds. Maximum memory usage: 15000 KB.",
                CreatorId = 3, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 4,
                GameId = 4,
                TournamentTitle = "Chess Grandmaster Tournament",
                Description =
                    "Strategize and checkmate your opponent's king in this prestigious Chess Grandmaster Tournament. Compete with the best and showcase your strategic prowess.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 4, 5),
                Status = TournamentStatus.PLAYED,
                Constraints = "Each player has a total of 60 minutes for all moves. Maximum memory usage: 15000 KB.",
                CreatorId = 1, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 5,
                GameId = 5,
                TournamentTitle = "Monopoly Tycoon Tournament",
                Description =
                    "Buy, trade, and manage properties to bankrupt your opponents in the Monopoly Tycoon Tournament. Compete to become the ultimate tycoon.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 5, 20),
                Status = TournamentStatus.PLAYED,
                Constraints = "Each turn must be completed within 2 minutes. Maximum memory usage: 15000 KB.",
                CreatorId = 1, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 6,
                GameId = 6,
                TournamentTitle = "Scrabble Wordsmith Showdown",
                Description =
                    "Create words on the board with letter tiles to score points in the Scrabble Wordsmith Showdown. Showcase your vocabulary and strategic skills.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 6, 15),
                Status = TournamentStatus.PLAYED,
                Constraints = "Each turn must be completed within 2 minutes. Maximum memory usage: 15000 KB.",
                CreatorId = 3, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 7,
                GameId = 7,
                TournamentTitle = "Checkers Championship",
                Description =
                    "Capture all of your opponent's pieces by jumping over them in the Checkers Championship. Test your strategy and tactics in this classic game.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 7, 1),
                Status = TournamentStatus.PLAYED,
                Constraints = "Each move must be made within 10 seconds. Maximum memory usage: 15000 KB.",
                CreatorId = 2, MemoryLimit = 15000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=")
            },
            new()
            {
                Id = 8,
                GameId = 8,
                TournamentTitle = "Backgammon Battle Royale",
                Description =
                    "Move all your pieces off the board before your opponent does in the Backgammon Battle Royale. Engage in this strategic and timeless game.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 8, 10),
                Status = TournamentStatus.PLAYED,
                Constraints = "Each move must be made within 30 seconds. Maximum memory usage: 15000 KB.",
                CreatorId = 2, MemoryLimit = 15000, TimeLimit = 2000,
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
                Id = 1, GameId = 1, PlayerId = 1, BotFile = "blackjack_bot_1", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 2, GameId = 1, PlayerId = 2, BotFile = "blackjack_bot_2", FileId = 3, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 3, GameId = 1, PlayerId = 3, BotFile = "blackjack_bot_3", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 4, GameId = 1, PlayerId = 4, BotFile = "blackjack_bot_4", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 5, GameId = 1, PlayerId = 5, BotFile = "blackjack_bot_5", FileId = 4, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 6, GameId = 1, PlayerId = 6, BotFile = "blackjack_bot_6", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 7, GameId = 1, PlayerId = 7, BotFile = "blackjack_bot_7", FileId = 3, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 8, GameId = 1, PlayerId = 8, BotFile = "blackjack_bot_8", FileId = 4, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 9, GameId = 1, PlayerId = 9, BotFile = "blackjack_bot_9", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 10, GameId = 1, PlayerId = 10, BotFile = "blackjack_bot_10", FileId = 4, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 11, GameId = 2, PlayerId = 2, BotFile = "zero_card_bot_1", FileId = 9, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 12, GameId = 2, PlayerId = 3, BotFile = "zero_card_bot_2", FileId = 10, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 13, GameId = 2, PlayerId = 4, BotFile = "zero_card_bot_3", FileId = 11, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 14, GameId = 2, PlayerId = 5, BotFile = "zero_card_bot_4", FileId = 9, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 15, GameId = 2, PlayerId = 6, BotFile = "zero_card_bot_5", FileId = 10, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 16, GameId = 2, PlayerId = 7, BotFile = "zero_card_bot_6", FileId = 11, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 17, GameId = 2, PlayerId = 8, BotFile = "zero_card_bot_7", FileId = 8, MemoryUsed = 8000,
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