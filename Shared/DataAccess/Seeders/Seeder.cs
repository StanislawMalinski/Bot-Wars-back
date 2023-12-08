﻿using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Seeders;

public class Seeder
{
    public static List<Player> GeneratePlayers()
    {
        return new List<Player>
        {
            new Player() { Id = 1, Email = "john.doe@example.com", Login = "john_doe", Points = 100, HashedPassword = "aasdsdas" },
            new Player() { Id = 2, Email = "jane.smith@example.com", Login = "jane_smith", Points = 150, HashedPassword = "sdfgdfg" },
            new Player() { Id = 3, Email = "alex.jones@example.com", Login = "alex_jones", Points = 200, HashedPassword = "hjklhjk" },
            new Player() { Id = 4, Email = "emily.white@example.com", Login = "emily_white", Points = 75, HashedPassword = "qwertyui" },
            new Player() { Id = 5, Email = "sam.wilson@example.com", Login = "sam_wilson", Points = 120, HashedPassword = "zxcvbnm" },
            new Player() { Id = 6, Email = "olivia.brown@example.com", Login = "olivia_brown", Points = 180, HashedPassword = "poiuytre" },
            new Player() { Id = 7, Email = "david.miller@example.com", Login = "david_miller", Points = 90, HashedPassword = "lkjhgfds" },
            new Player() { Id = 8, Email = "emma.jenkins@example.com", Login = "emma_jenkins", Points = 160, HashedPassword = "mnbvcxz" },
            new Player() { Id = 9, Email = "ryan.clark@example.com", Login = "ryan_clark", Points = 110, HashedPassword = "asdfghjk" },
            new Player() { Id = 10, Email = "sara.taylor@example.com", Login = "sara_taylor", Points = 130, HashedPassword = "qazwsxed" }
        };
    }

    public static List<Game> GenerateGames()
    {
        return new List<Game>
            {
                new Game() { Id = 1, NumbersOfPlayer = 10, LastModification = DateTime.Now, GameFile = "Quake III Arena", GameInstructions = "Eliminate the enemy players in fast-paced multiplayer battles.", InterfaceDefinition = "First-Person Shooter (FPS)", IsAvailableForPlay = true },
                new Game() { Id = 2, NumbersOfPlayer = 1, LastModification = DateTime.Now, GameFile = "The Legend of Zelda: Breath of the Wild", GameInstructions = "Embark on an epic adventure to defeat the Calamity Ganon and save Hyrule.", InterfaceDefinition = "Action-Adventure", IsAvailableForPlay = true },
                new Game() { Id = 3, NumbersOfPlayer = 2, LastModification = DateTime.Now, GameFile = "FIFA 22", GameInstructions = "Experience realistic football simulation with updated teams and gameplay.", InterfaceDefinition = "Sports Simulation", IsAvailableForPlay = true },
                new Game() { Id = 4, NumbersOfPlayer = 7, LastModification = DateTime.Now, GameFile = "Among Us", GameInstructions = "Work together to complete tasks while identifying the impostors among the crew.", InterfaceDefinition = "Social Deduction", IsAvailableForPlay = true },
                new Game() { Id = 5, NumbersOfPlayer = 16, LastModification = DateTime.Now, GameFile = "Minecraft", GameInstructions = "Build and explore a blocky world, mine resources, and survive.", InterfaceDefinition = "Sandbox", IsAvailableForPlay = false },
                new Game() { Id = 6, NumbersOfPlayer = 1, LastModification = DateTime.Now, GameFile = "Cyberpunk 2077", GameInstructions = "Navigate the futuristic open world of Night City as the mercenary V.", InterfaceDefinition = "Action RPG", IsAvailableForPlay = true },
                new Game() { Id = 7, NumbersOfPlayer = 14, LastModification = DateTime.Now, GameFile = "Rocket League", GameInstructions = "Play soccer with rocket-powered cars in this unique sports game.", InterfaceDefinition = "Vehicular Soccer", IsAvailableForPlay = true },
                new Game() { Id = 8, NumbersOfPlayer = 8, LastModification = DateTime.Now, GameFile = "Call of Duty: Warzone", GameInstructions = "Engage in intense battle royale action in the Call of Duty universe.", InterfaceDefinition = "First-Person Shooter (Battle Royale)", IsAvailableForPlay = false },
                new Game() { Id = 9, NumbersOfPlayer = 5, LastModification = DateTime.Now, GameFile = "Animal Crossing: New Horizons", GameInstructions = "Create and customize your own island paradise in a relaxing simulation.", InterfaceDefinition = "Life Simulation", IsAvailableForPlay = true },
                new Game() { Id = 10, NumbersOfPlayer = 10, LastModification = DateTime.Now, GameFile = "Dota 2", GameInstructions = "Compete in strategic team-based battles in this multiplayer online battle arena (MOBA).", InterfaceDefinition = "MOBA", IsAvailableForPlay = true }
            };
    }

    public static List<Tournament> GenerateTournaments()
    {
        return new List<Tournament>()
        {
            new Tournament()
            {
                Id = 1,
                GameId = 1,
                TournamentTitle = "Quake III Arena Championship",
                Description = "Compete in the ultimate Quake III Arena tournament and prove your skills in fast-paced multiplayer battles.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 1, 20),
                WasPlayedOut = false,
                Constraints = "Participants must have a minimum skill level of intermediate.",
                Image = "quakethreearena.jpg" // You can replace this with the actual image file or URL
            },
            new Tournament()
            {
                Id = 2,
                GameId = 2,
                TournamentTitle = "Zelda Master Cup",
                Description = "Embark on a quest to become the master of The Legend of Zelda: Breath of the Wild. Solve puzzles and defeat foes to claim victory.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 2, 15),
                WasPlayedOut = false,
                Constraints = "Participants must complete the game on a specific difficulty level.",
                Image = "zeldabreathofthewild.jpg" // You can replace this with the actual image file or URL
            },
            new Tournament()
            {
                Id = 3,
                GameId = 3,
                TournamentTitle = "FIFA 22 World Cup",
                Description = "Experience the thrill of virtual football in the FIFA 22 World Cup. Compete with players from around the globe for the championship.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 3, 10),
                WasPlayedOut = false,
                Constraints = "Teams must consist of real-world players.",
                Image = "fifa22worldcup.jpg" // You can replace this with the actual image file or URL
            },
            new Tournament()
            {
                Id = 4,
                GameId = 4,
                TournamentTitle = "Among Us Infiltration Challenge",
                Description = "Test your deception skills in the Among Us Infiltration Challenge. Work as a crew member or impostor to secure victory.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2022, 4, 5),
                WasPlayedOut = true,
                Constraints = "Players must use voice communication during the game.",
                Image = "amongusinfiltration.jpg" // You can replace this with the actual image file or URL
            },
            new Tournament()
            {
                Id = 5,
                GameId = 5,
                TournamentTitle = "Minecraft Building Showcase",
                Description = "Showcase your creative building skills in the Minecraft Building Showcase. Construct impressive structures and compete for recognition.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 5, 20),
                WasPlayedOut = false,
                Constraints = "Builds must adhere to a specific theme.",
                Image = "minecraftbuildingshowcase.jpg" // You can replace this with the actual image file or URL
            },
            new Tournament()
            {
                Id = 6,
                GameId = 6,
                TournamentTitle = "Cyberpunk 2077 Cyberwarfare Challenge",
                Description = "Immerse yourself in the cyberpunk world of Night City. Compete in cyberwarfare challenges and emerge as the ultimate netrunner.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2022, 6, 15),
                WasPlayedOut = true,
                Constraints = "Participants must customize their character's appearance.",
                Image = "cyberpunk2077challenge.jpg" // You can replace this with the actual image file or URL
            },
            new Tournament()
            {
                Id = 7,
                GameId = 7,
                TournamentTitle = "Rocket League Championship",
                Description = "Take part in high-flying, rocket-powered soccer action. Compete in the Rocket League Championship and score goals to victory.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 7, 1),
                WasPlayedOut = false,
                Constraints = "Teams must consist of three players.",
                Image = "rocketleaguechampionship.jpg" // You can replace this with the actual image file or URL
            },
            new Tournament()
            {
                Id = 8,
                GameId = 8,
                TournamentTitle = "Call of Duty: Warzone Battle Royale",
                Description = "Join the intense battle royale action in Call of Duty: Warzone. Compete against other squads to be the last team standing.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 8, 10),
                WasPlayedOut = false,
                Constraints = "Players must adhere to the battle royale ruleset.",
                Image = "callofdutywarzone.jpg" // You can replace this with the actual image file or URL
            },
            new Tournament(){
                Id = 9,
                GameId = 9,
                TournamentTitle = "Animal Crossing Island Showcase",
                Description = "Create the most charming and unique island paradise in the Animal Crossing Island Showcase. Display your creativity and win accolades.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 9, 5),
                WasPlayedOut = false,
                Constraints = "Islands must be designed within a specific theme.",
                Image = "animalcrossingislandshowcase.jpg" // You can replace this with the actual image file or URL
            },
            new Tournament()
            {
                Id = 10,
                GameId = 10,
                TournamentTitle = "Dota 2 Clash of Titans",
                Description = "Enter the world of strategic battles in the Dota 2 Clash of Titans. Assemble your team, choose your heroes, and conquer the opposition.",
                PostedDate = DateTime.Now,
                TournamentsDate = new DateTime(2023, 10, 20),
                WasPlayedOut = false,
                Constraints = "Teams must adhere to the standard Dota 2 competitive rules.",
                Image = "dota2clashoftitans.jpg" // You can replace this with the actual image file or URL
            }
        };
    }

    public static List<Bot> GenerateBots()
    {
        return new List<Bot>()
        {
            new Bot() { Id = 1, GameId = 1, PlayerId = 1, BotFile = "quake3_bot_1" },
            new Bot() { Id = 2, GameId = 1, PlayerId = 2, BotFile = "quake3_bot_2" },
            new Bot() { Id = 3, GameId = 2, PlayerId = 3, BotFile = "zelda_bot_1" },
            new Bot() { Id = 4, GameId = 2, PlayerId = 4, BotFile = "zelda_bot_2" },
            new Bot() { Id = 5, GameId = 3, PlayerId = 5, BotFile = "fifa22_bot_1" },
            new Bot() { Id = 6, GameId = 3, PlayerId = 6, BotFile = "fifa22_bot_2" },
            new Bot() { Id = 7, GameId = 4, PlayerId = 7, BotFile = "amongus_bot_1" },
            new Bot() { Id = 8, GameId = 4, PlayerId = 8, BotFile = "amongus_bot_2" },
            new Bot() { Id = 9, GameId = 5, PlayerId = 9, BotFile = "minecraft_bot_1" },
            new Bot() { Id = 10, GameId = 5, PlayerId = 10, BotFile = "minecraft_bot_2" }
        };
    }

    public static List<TournamentReference> GenerateTournamentReferences()
    {
        return new List<TournamentReference>()
        {
            new TournamentReference() { Id = 1, tournamentId = 1, botId = 1 },
            new TournamentReference() { Id = 2, tournamentId = 1, botId = 2 },
            new TournamentReference() { Id = 3, tournamentId = 2, botId = 3 },
            new TournamentReference() { Id = 4, tournamentId = 2, botId = 4 },
            new TournamentReference() { Id = 5, tournamentId = 5, botId = 10 },
        };
    }
}