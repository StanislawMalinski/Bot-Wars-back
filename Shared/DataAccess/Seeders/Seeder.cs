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
                GameInstructions = "Gra Black Jack opiera się na implementacji popularnej gry karcianej o tej samej nazwie. Boty będą grać ze sobą 100 rund gdzie w każdej obstawiają po 1 punkcie.\nBot który będzie miał po 100 rundzie więcej punktów wygrywa. W przypadku remisu rozegrane dodatkowe rundy aż ilość punktów będzie różna. Boty będą w swojej rundzie dostawać informacje o planszy w taki sposób \n9_Diamonds 7_Clubs 8_Spades d: Jack_Spades \nKarty są zdefiniowane w następujący sposób\n{\"2\", \"3\", \"4\", \"5\", \"6\", \"7\", \"8\", \"9\", \"10\", \"Jack\", \"Queen\", \"King\", \"Ace\"}\n {\"Hearts\", \"Diamonds\", \"Clubs\", \"Spades\"}\nNajpier będa pokazane kary uzytkowniak a potem po znaku d: karty dealera. Po tym użytkownik ma następujące możliwości: \nhit, stand, double, insurance, surrender, split\nodpowiadają one komendom z gry Dokładniejszy ich opis można znaleźć na stronie z zasadami np. https://www.venetianlasvegas.com/casino/table-games/how-to-play-blackjack.html\n",
                InterfaceDefinition = "Gra kraciana", IsAvailableForPlay = true, FileId = 5,
                CreatorId = 1, Language = Language.C
            },
            new()
            {
                Id = 2, NumbersOfPlayer = 1, LastModification = DateTime.Now,
                GameFile = "Zero",
                GameInstructions = "Zero zasady: każdy z graczy ma 9 kart, na stole leży 5 kart. W swojej turze każdy gracz może wymienić 1 kartę z ręki z 1 kartą ze stołu lub spasować. Gra kończy się gdy ktoś zdobędzie układ dający 0 punktów, lub 2 razy ktoś spasuje. Jeśli będą 2 pasy, dogrywa się kolejkę do końca aby wszyscy mieli tyle samo tur. Punkty liczy się następująco: każda unikalna wartość karty jest ma tyle punktów jaką ma wartość, 3 karty z wartością 7 mają nadal 7 punktów. Ponadto 5 lub więcej kart z tą samą cechą (kolorem lub wartością) są warte 0 punktów. Przykładowo 5 kart z wartością 7 mają 0 punktów. Celem gry jest zdobycie MNIEJ punktów od innych graczy (tutaj jednego gracza).\nGra ma następujące kolory: ['Yellow', 'Green', 'Blue', 'Red', 'Pink', 'White', 'Brown'] i wartości: ['1', '2', '3', '4', '5', '6', '7', '8']. Bot dostanie jako wejście 9 kart na ręce i 5 kart stole w takim formacie: \"7 of Red;1 of Yellow;7 of Pink;5 of Brown;1 of Blue;2 of Brown;5 of Red;7 of Green;7 of White; Table: 4 of Green;6 of Yellow;6 of White;4 of Yellow;6 of Brown;\" a odpowiada 1) parą kart które wymienia, najpierw z ręki a potem ze stołu (liczba 0-8 spacja liczba 0-4) 2) \"fold\" jeśli chce spasować.\n",
                InterfaceDefinition = "Gra kraciana", IsAvailableForPlay = true, FileId = 12, CreatorId = 1,
                Language = Language.PYTHON
            },
            new()
            {
                Id = 3, NumbersOfPlayer = 2, LastModification = DateTime.Now, GameFile = "Chess",
                GameInstructions = "Kalsyczne szachu potowi będą przekazywanie ruchy peciwnika po czym on sam ma zwrócić swój ruch jeśli bot miałby się ruszyć pierwszy otrzyma na start -1 -1.\nPola są opisywane w postaci dwóch znaków litery i cyfry na przykład a1, a2 e5.\nPlansza jest definiowana jako pole od a1 - h1 do a8 - h8 figury pionków biały znajdują się na pozycjach od a1 do h1 i a2 h2 natomiast pionków czarnych na a8 -h8 i a7-h7 rozłożenie figur jest zgodne z standardem szachowym.\nRuch odbywa się poprzez podanie pozycji figury po czym podanie pozycji na którą ma się przemieścić  na przykład”  a2 a3  “ byłby to ruch białego piona do przodu . Nieprawidłowy ruch kończy się przegraną bota.\nAwansowanie piona jest automatycznym awansowaniem do królowej.\n",
                InterfaceDefinition = "Szachu ruchy format ruchów pgn", IsAvailableForPlay = true, FileId = 13, CreatorId = 1,
                Language = Language.C
            },
            new()
            {
                Id = 4, NumbersOfPlayer = 7, LastModification = DateTime.Now, GameFile = "Among Us",
                GameInstructions = "",
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
                TournamentTitle = "Black_Jack",
                Description =
                    "Truniej w popularna gre karcianą BlackJack",
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
                TournamentTitle = "Zero",
                Description =
                    "Truniej w popularna gre karcianą Zero.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 2, 15),
                Status = TournamentStatus.PLAYED,
                Constraints = "Participants must complete the game on a specific difficulty level.",
                CreatorId = 2, MemoryLimit = 150000, TimeLimit = 2000,
                Image = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=") // You can replace this with the actual image file or URL
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
                    "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII=") // You can replace this with the actual image file or URL
            },
            new()
            {
                Id = 4,
                GameId = 2,
                TournamentTitle = "Among Us Infiltration Challenge",
                Description =
                    "this game not exist",
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
                    "this game not exist",
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
                    "this game not exist",
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
                    "this game not exist",
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
                    "ds",
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
                    "this game not exist",
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
                    "this game not exist",
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
                    "this game not exist",
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
                Id = 17, GameId = 2, PlayerId = 8, BotFile = "bot_zero_5.java", FileId = 8, MemoryUsed = 8000,
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