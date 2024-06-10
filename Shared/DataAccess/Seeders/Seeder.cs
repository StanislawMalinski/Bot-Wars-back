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
                Id = 1, NumbersOfPlayer = 10, LastModification = DateTime.Now, GameFile = "Black_Jack",
                GameInstructions = "Blackjack game is based on the implementation of the popular card game of the same name. Bots will play 100 rounds with each betting 1 point per round.\nThe bot with more points after 100 rounds wins. In case of a tie, additional rounds will be played until the points difference is reached. Bots will receive information about the board in their round in the following format:\n9_Diamonds 7_Clubs 8_Spades d: Jack_Spades\nCards are defined as follows:\n{\"2\", \"3\", \"4\", \"5\", \"6\", \"7\", \"8\", \"9\", \"10\", \"Jack\", \"Queen\", \"King\", \"Ace\"}\n {\"Hearts\", \"Diamonds\", \"Clubs\", \"Spades\"}\nFirst, the user's cards will be shown, then after the 'd:' sign, the dealer's cards. After this, the user has the following options: \nhit, stand, double, insurance, surrender, split\nThey correspond to commands in the game. More detailed descriptions can be found on the rules page, for example: https://www.venetianlasvegas.com/casino/table-games/how-to-play-blackjack.html\n",
                InterfaceDefinition = "Classical Card game, Gambling", IsAvailableForPlay = true, FileId = 5,
                CreatorId = 1, Language = Language.C
            },
            new()
            {
                Id = 2, NumbersOfPlayer = 1, LastModification = DateTime.Now,
                GameFile = "Zero",
                GameInstructions = "Zero rules: each player has 9 cards, and there are 5 cards on the table. In their turn, each player can exchange 1 card from their hand with 1 card from the table or fold. The game ends when someone gets a combination giving 0 points, or when someone folds twice. If there are 2 folds, an additional round is played until all players have had the same number of turns. Points are counted as follows: each unique card value is worth the same number of points as its value; 3 cards with a value of 7 still have 7 points. Additionally, 5 or more cards with the same feature (color or value) are worth 0 points. For example, 5 cards with a value of 7 are worth 0 points. The goal of the game is to get FEWER points than the other player.\nThe game has the following colors: ['Yellow', 'Green', 'Blue', 'Red', 'Pink', 'White', 'Brown'] and values: ['1', '2', '3', '4', '5', '6', '7', '8']. The bot will receive 9 cards in hand and 5 cards on the table in the following format: \"7 of Red;1 of Yellow;7 of Pink;5 of Brown;1 of Blue;2 of Brown;5 of Red;7 of Green;7 of White; Table: 4 of Green;6 of Yellow;6 of White;4 of Yellow;6 of Brown;\" and will respond with 1) pairs of cards to exchange, first from the hand and then from the table (number 0-8 space number 0-4) 2) \"fold\" if it wants to fold.\n",
                InterfaceDefinition = "Card game", IsAvailableForPlay = true, FileId = 12, CreatorId = 1,
                Language = Language.PYTHON
            },
            new()
            {
                Id = 3, NumbersOfPlayer = 2, LastModification = DateTime.Now, GameFile = "Chess",
                GameInstructions = "Classic chess game where moves are passed between opponents, then the bot must return its move. If the bot would move first, it will start with -1 -1.\nFields are described in the form of two characters: a letter and a number, for example, a1, a2, e5.\nThe board is defined as fields from a1 - h1 to a8 - h8, with white pieces located from a1 to h1 and a2 to h2, and black pieces from a8 to h8 and a7 to h7. The arrangement of pieces follows the standard chess convention.\nMoves are made by specifying the position of the piece and then the position to move to, for example, \"a2 a3\" would be a move of a white pawn forward. An invalid move results in the bot's loss.\nPawn promotion is automatic, promoting to a queen.\n",
                InterfaceDefinition = "Abstract Strategy, Chess, moves in PGN format", IsAvailableForPlay = true, FileId = 13, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 4, NumbersOfPlayer = 2, LastModification = DateTime.Now, GameFile = "Chess 2.0",
                GameInstructions = "Strategize and checkmate your opponent's king in new, better version of Chess!",
                InterfaceDefinition = "Abstract Strategy", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
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
                InterfaceDefinition = "Abstract Strategy, Board Game", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 8, NumbersOfPlayer = 2, LastModification = DateTime.Now, GameFile = "Backgammon",
                GameInstructions = "Move all your pieces off the board before your opponent does.",
                InterfaceDefinition = "Abstract Strategy, Board Game", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 9, NumbersOfPlayer = 4, LastModification = DateTime.Now, GameFile = "Bridge",
                GameInstructions = "Compete in teams to win tricks and score points.",
                InterfaceDefinition = "Classical Card Game", IsAvailableForPlay = true, FileId = 1, CreatorId = 1,
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
                TournamentTitle = "Chess",
                Description =
                    "Klasyczny turniej w szachy botów.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 3, 10),
                Status = TournamentStatus.PLAYED,
                Constraints = "Teams must consist of real-world players.",
                CreatorId = 3, MemoryLimit = 150000, TimeLimit = 4000,
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
                Id = 1, GameId = 1, PlayerId = 1, BotFile = "Back_jackbot_1", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 2, GameId = 1, PlayerId = 2, BotFile = "Back_jackbot_2", FileId = 3, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 3, GameId = 1, PlayerId = 3, BotFile = "Back_jackbot_3", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 4, GameId = 1, PlayerId = 4, BotFile = "Back_jackbot_4", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 5, GameId = 1, PlayerId = 5, BotFile = "Back_jackbot_5", FileId = 4, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 6, GameId = 1, PlayerId = 6, BotFile = "Back_jackbot_6", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 7, GameId = 1, PlayerId = 7, BotFile = "Back_jackbot_7", FileId = 3, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 8, GameId = 1, PlayerId = 8, BotFile = "Back_jackbot_8", FileId = 4, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 9, GameId = 1, PlayerId = 9, BotFile = "Back_jackbot_9", FileId = 2, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 10, GameId = 1, PlayerId = 10, BotFile = "Back_jackbot_10", FileId = 4, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 11, GameId = 2, PlayerId = 2, BotFile = "Zero_bot_1", FileId = 9, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 12, GameId = 2, PlayerId = 3, BotFile = "Zero_bot_1", FileId = 10, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 13, GameId = 2, PlayerId = 4, BotFile = "Zero_bot_2", FileId = 11, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 14, GameId = 2, PlayerId = 5, BotFile = "Zero_bot_3", FileId = 9, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 15, GameId = 2, PlayerId = 6, BotFile = "Zero_bot_4", FileId = 10, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.PYTHON, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 16, GameId = 2, PlayerId = 7, BotFile = "Zero_bot_6", FileId = 11, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 17, GameId = 2, PlayerId = 8, BotFile = "zero_card_bot_7", FileId = 8, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.Java, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 18, GameId = 3, PlayerId = 1, BotFile = "chess_bot1.cpp", FileId = 14, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 19, GameId = 3, PlayerId = 2, BotFile = "chess_bot2.cpp", FileId = 15, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 20, GameId = 3, PlayerId = 3, BotFile = "chess_bot2.cpp", FileId = 16, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 21, GameId = 3, PlayerId = 4, BotFile = "chess_bot1.cpp", FileId = 14, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            },
            new()
            {
                Id = 22, GameId = 3, PlayerId = 5, BotFile = "chess_bot1.cpp", FileId = 14, MemoryUsed = 8000,
                TimeUsed = 100, Language = Language.C, Validation = BotStatus.NotValidated
            }
        };
    }

    public static IEnumerable<TournamentReference> GenerateTournamentReferences()
    {
        return new List<TournamentReference>
        {
            new() { Id = 1, tournamentId = 1, botId = 1 },
            new() { Id = 2, tournamentId = 1, botId = 2 },
            new() { Id = 3, tournamentId = 4, botId = 3 },
            new() { Id = 4, tournamentId = 4, botId = 4 },
            new() { Id = 5, tournamentId = 4, botId = 10 },
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
            new() { Id = 17, tournamentId = 2, botId = 17 },
            new() { Id = 18, tournamentId = 3, botId = 18 },
            new() { Id = 19, tournamentId = 3, botId = 19 },
            new() { Id = 20, tournamentId = 3, botId = 20 },
            new() { Id = 21, tournamentId = 3, botId = 21 },
            new() { Id = 22, tournamentId = 3, botId = 22 }
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