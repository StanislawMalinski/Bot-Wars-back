﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.DataAccess.Context;

#nullable disable

namespace Shared.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231209174811_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.ArchivedMatchPlayers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("MatchId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("ArchivedMatchPlayers");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.ArchivedMatches", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("Match")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Played")
                        .HasColumnType("datetime2");

                    b.Property<long>("TournamentsId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("TournamentsId");

                    b.ToTable("ArchivedMatches");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.Bot", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("BotFile")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Bots");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            BotFile = "quake3_bot_1",
                            GameId = 1L,
                            PlayerId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            BotFile = "quake3_bot_2",
                            GameId = 1L,
                            PlayerId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            BotFile = "zelda_bot_1",
                            GameId = 2L,
                            PlayerId = 3L
                        },
                        new
                        {
                            Id = 4L,
                            BotFile = "zelda_bot_2",
                            GameId = 2L,
                            PlayerId = 4L
                        },
                        new
                        {
                            Id = 5L,
                            BotFile = "fifa22_bot_1",
                            GameId = 3L,
                            PlayerId = 5L
                        },
                        new
                        {
                            Id = 6L,
                            BotFile = "fifa22_bot_2",
                            GameId = 3L,
                            PlayerId = 6L
                        },
                        new
                        {
                            Id = 7L,
                            BotFile = "amongus_bot_1",
                            GameId = 4L,
                            PlayerId = 7L
                        },
                        new
                        {
                            Id = 8L,
                            BotFile = "amongus_bot_2",
                            GameId = 4L,
                            PlayerId = 8L
                        },
                        new
                        {
                            Id = 9L,
                            BotFile = "minecraft_bot_1",
                            GameId = 5L,
                            PlayerId = 9L
                        },
                        new
                        {
                            Id = 10L,
                            BotFile = "minecraft_bot_2",
                            GameId = 5L,
                            PlayerId = 10L
                        });
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("GameFile")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("GameInstructions")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("InterfaceDefinition")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<bool>("IsAvailableForPlay")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModification")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumbersOfPlayer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            GameFile = "Quake III Arena",
                            GameInstructions = "Eliminate the enemy players in fast-paced multiplayer battles.",
                            InterfaceDefinition = "First-Person Shooter (FPS)",
                            IsAvailableForPlay = true,
                            LastModification = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8157),
                            NumbersOfPlayer = 10
                        },
                        new
                        {
                            Id = 2L,
                            GameFile = "The Legend of Zelda: Breath of the Wild",
                            GameInstructions = "Embark on an epic adventure to defeat the Calamity Ganon and save Hyrule.",
                            InterfaceDefinition = "Action-Adventure",
                            IsAvailableForPlay = true,
                            LastModification = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8221),
                            NumbersOfPlayer = 1
                        },
                        new
                        {
                            Id = 3L,
                            GameFile = "FIFA 22",
                            GameInstructions = "Experience realistic football simulation with updated teams and gameplay.",
                            InterfaceDefinition = "Sports Simulation",
                            IsAvailableForPlay = true,
                            LastModification = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8224),
                            NumbersOfPlayer = 2
                        },
                        new
                        {
                            Id = 4L,
                            GameFile = "Among Us",
                            GameInstructions = "Work together to complete tasks while identifying the impostors among the crew.",
                            InterfaceDefinition = "Social Deduction",
                            IsAvailableForPlay = true,
                            LastModification = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8227),
                            NumbersOfPlayer = 7
                        },
                        new
                        {
                            Id = 5L,
                            GameFile = "Minecraft",
                            GameInstructions = "Build and explore a blocky world, mine resources, and survive.",
                            InterfaceDefinition = "Sandbox",
                            IsAvailableForPlay = false,
                            LastModification = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8229),
                            NumbersOfPlayer = 16
                        },
                        new
                        {
                            Id = 6L,
                            GameFile = "Cyberpunk 2077",
                            GameInstructions = "Navigate the futuristic open world of Night City as the mercenary V.",
                            InterfaceDefinition = "Action RPG",
                            IsAvailableForPlay = true,
                            LastModification = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8233),
                            NumbersOfPlayer = 1
                        },
                        new
                        {
                            Id = 7L,
                            GameFile = "Rocket League",
                            GameInstructions = "Play soccer with rocket-powered cars in this unique sports game.",
                            InterfaceDefinition = "Vehicular Soccer",
                            IsAvailableForPlay = true,
                            LastModification = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8236),
                            NumbersOfPlayer = 14
                        },
                        new
                        {
                            Id = 8L,
                            GameFile = "Call of Duty: Warzone",
                            GameInstructions = "Engage in intense battle royale action in the Call of Duty universe.",
                            InterfaceDefinition = "First-Person Shooter (Battle Royale)",
                            IsAvailableForPlay = false,
                            LastModification = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8238),
                            NumbersOfPlayer = 8
                        },
                        new
                        {
                            Id = 9L,
                            GameFile = "Animal Crossing: New Horizons",
                            GameInstructions = "Create and customize your own island paradise in a relaxing simulation.",
                            InterfaceDefinition = "Life Simulation",
                            IsAvailableForPlay = true,
                            LastModification = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8241),
                            NumbersOfPlayer = 5
                        },
                        new
                        {
                            Id = 10L,
                            GameFile = "Dota 2",
                            GameInstructions = "Compete in strategic team-based battles in this multiplayer online battle arena (MOBA).",
                            InterfaceDefinition = "MOBA",
                            IsAvailableForPlay = true,
                            LastModification = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8244),
                            NumbersOfPlayer = 10
                        });
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<long>("Points")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "john.doe@example.com",
                            HashedPassword = "aasdsdas",
                            Login = "john_doe",
                            Points = 100L
                        },
                        new
                        {
                            Id = 2L,
                            Email = "jane.smith@example.com",
                            HashedPassword = "sdfgdfg",
                            Login = "jane_smith",
                            Points = 150L
                        },
                        new
                        {
                            Id = 3L,
                            Email = "alex.jones@example.com",
                            HashedPassword = "hjklhjk",
                            Login = "alex_jones",
                            Points = 200L
                        },
                        new
                        {
                            Id = 4L,
                            Email = "emily.white@example.com",
                            HashedPassword = "qwertyui",
                            Login = "emily_white",
                            Points = 75L
                        },
                        new
                        {
                            Id = 5L,
                            Email = "sam.wilson@example.com",
                            HashedPassword = "zxcvbnm",
                            Login = "sam_wilson",
                            Points = 120L
                        },
                        new
                        {
                            Id = 6L,
                            Email = "olivia.brown@example.com",
                            HashedPassword = "poiuytre",
                            Login = "olivia_brown",
                            Points = 180L
                        },
                        new
                        {
                            Id = 7L,
                            Email = "david.miller@example.com",
                            HashedPassword = "lkjhgfds",
                            Login = "david_miller",
                            Points = 90L
                        },
                        new
                        {
                            Id = 8L,
                            Email = "emma.jenkins@example.com",
                            HashedPassword = "mnbvcxz",
                            Login = "emma_jenkins",
                            Points = 160L
                        },
                        new
                        {
                            Id = 9L,
                            Email = "ryan.clark@example.com",
                            HashedPassword = "asdfghjk",
                            Login = "ryan_clark",
                            Points = 110L
                        },
                        new
                        {
                            Id = 10L,
                            Email = "sara.taylor@example.com",
                            HashedPassword = "qazwsxed",
                            Login = "sara_taylor",
                            Points = 130L
                        });
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.PointHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("Gain")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LogDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Loss")
                        .HasColumnType("bigint");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PointHistories");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.Tournament", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Constraints")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayersLimit")
                        .HasColumnType("int");

                    b.Property<DateTime>("PostedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TournamentTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("TournamentsDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("WasPlayedOut")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Tournaments");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Constraints = "Participants must have a minimum skill level of intermediate.",
                            Description = "Compete in the ultimate Quake III Arena tournament and prove your skills in fast-paced multiplayer battles.",
                            GameId = 1L,
                            Image = "quakethreearena.jpg",
                            PlayersLimit = 0,
                            PostedDate = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9603),
                            TournamentTitle = "Quake III Arena Championship",
                            TournamentsDate = new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WasPlayedOut = false
                        },
                        new
                        {
                            Id = 2L,
                            Constraints = "Participants must complete the game on a specific difficulty level.",
                            Description = "Embark on a quest to become the master of The Legend of Zelda: Breath of the Wild. Solve puzzles and defeat foes to claim victory.",
                            GameId = 2L,
                            Image = "zeldabreathofthewild.jpg",
                            PlayersLimit = 0,
                            PostedDate = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9658),
                            TournamentTitle = "Zelda Master Cup",
                            TournamentsDate = new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WasPlayedOut = false
                        },
                        new
                        {
                            Id = 3L,
                            Constraints = "Teams must consist of real-world players.",
                            Description = "Experience the thrill of virtual football in the FIFA 22 World Cup. Compete with players from around the globe for the championship.",
                            GameId = 3L,
                            Image = "fifa22worldcup.jpg",
                            PlayersLimit = 0,
                            PostedDate = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9671),
                            TournamentTitle = "FIFA 22 World Cup",
                            TournamentsDate = new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WasPlayedOut = false
                        },
                        new
                        {
                            Id = 4L,
                            Constraints = "Players must use voice communication during the game.",
                            Description = "Test your deception skills in the Among Us Infiltration Challenge. Work as a crew member or impostor to secure victory.",
                            GameId = 4L,
                            Image = "amongusinfiltration.jpg",
                            PlayersLimit = 0,
                            PostedDate = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9682),
                            TournamentTitle = "Among Us Infiltration Challenge",
                            TournamentsDate = new DateTime(2022, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WasPlayedOut = true
                        },
                        new
                        {
                            Id = 5L,
                            Constraints = "Builds must adhere to a specific theme.",
                            Description = "Showcase your creative building skills in the Minecraft Building Showcase. Construct impressive structures and compete for recognition.",
                            GameId = 5L,
                            Image = "minecraftbuildingshowcase.jpg",
                            PlayersLimit = 0,
                            PostedDate = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9693),
                            TournamentTitle = "Minecraft Building Showcase",
                            TournamentsDate = new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WasPlayedOut = false
                        },
                        new
                        {
                            Id = 6L,
                            Constraints = "Participants must customize their character's appearance.",
                            Description = "Immerse yourself in the cyberpunk world of Night City. Compete in cyberwarfare challenges and emerge as the ultimate netrunner.",
                            GameId = 6L,
                            Image = "cyberpunk2077challenge.jpg",
                            PlayersLimit = 0,
                            PostedDate = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9711),
                            TournamentTitle = "Cyberpunk 2077 Cyberwarfare Challenge",
                            TournamentsDate = new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WasPlayedOut = true
                        },
                        new
                        {
                            Id = 7L,
                            Constraints = "Teams must consist of three players.",
                            Description = "Take part in high-flying, rocket-powered soccer action. Compete in the Rocket League Championship and score goals to victory.",
                            GameId = 7L,
                            Image = "rocketleaguechampionship.jpg",
                            PlayersLimit = 0,
                            PostedDate = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9724),
                            TournamentTitle = "Rocket League Championship",
                            TournamentsDate = new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WasPlayedOut = false
                        },
                        new
                        {
                            Id = 8L,
                            Constraints = "Players must adhere to the battle royale ruleset.",
                            Description = "Join the intense battle royale action in Call of Duty: Warzone. Compete against other squads to be the last team standing.",
                            GameId = 8L,
                            Image = "callofdutywarzone.jpg",
                            PlayersLimit = 0,
                            PostedDate = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9729),
                            TournamentTitle = "Call of Duty: Warzone Battle Royale",
                            TournamentsDate = new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WasPlayedOut = false
                        },
                        new
                        {
                            Id = 9L,
                            Constraints = "Islands must be designed within a specific theme.",
                            Description = "Create the most charming and unique island paradise in the Animal Crossing Island Showcase. Display your creativity and win accolades.",
                            GameId = 9L,
                            Image = "animalcrossingislandshowcase.jpg",
                            PlayersLimit = 0,
                            PostedDate = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9734),
                            TournamentTitle = "Animal Crossing Island Showcase",
                            TournamentsDate = new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WasPlayedOut = false
                        },
                        new
                        {
                            Id = 10L,
                            Constraints = "Teams must adhere to the standard Dota 2 competitive rules.",
                            Description = "Enter the world of strategic battles in the Dota 2 Clash of Titans. Assemble your team, choose your heroes, and conquer the opposition.",
                            GameId = 10L,
                            Image = "dota2clashoftitans.jpg",
                            PlayersLimit = 0,
                            PostedDate = new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9747),
                            TournamentTitle = "Dota 2 Clash of Titans",
                            TournamentsDate = new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WasPlayedOut = false
                        });
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.TournamentReference", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("LastModification")
                        .HasColumnType("datetime2");

                    b.Property<long>("botId")
                        .HasColumnType("bigint");

                    b.Property<long>("tournamentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("botId");

                    b.HasIndex("tournamentId");

                    b.ToTable("TournamentReferences");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            LastModification = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            botId = 1L,
                            tournamentId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            LastModification = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            botId = 2L,
                            tournamentId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            LastModification = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            botId = 3L,
                            tournamentId = 2L
                        },
                        new
                        {
                            Id = 4L,
                            LastModification = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            botId = 4L,
                            tournamentId = 2L
                        },
                        new
                        {
                            Id = 5L,
                            LastModification = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            botId = 10L,
                            tournamentId = 5L
                        });
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.ArchivedMatchPlayers", b =>
                {
                    b.HasOne("Shared.DataAccess.DataBaseEntities.ArchivedMatches", "archivedMatches")
                        .WithMany("ArchivedMatchPlayers")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.DataAccess.DataBaseEntities.Player", "Player")
                        .WithMany("ArchivedMatchPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("archivedMatches");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.ArchivedMatches", b =>
                {
                    b.HasOne("Shared.DataAccess.DataBaseEntities.Game", "Game")
                        .WithMany("ArchivedMatches")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.DataAccess.DataBaseEntities.Tournament", "Tournament")
                        .WithMany("ArchivedMatches")
                        .HasForeignKey("TournamentsId")
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.Bot", b =>
                {
                    b.HasOne("Shared.DataAccess.DataBaseEntities.Game", "Games")
                        .WithMany("Bot")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.DataAccess.DataBaseEntities.Player", "Player")
                        .WithMany("Bot")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Games");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.PointHistory", b =>
                {
                    b.HasOne("Shared.DataAccess.DataBaseEntities.Player", "Player")
                        .WithMany("PlayerPointsList")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.Tournament", b =>
                {
                    b.HasOne("Shared.DataAccess.DataBaseEntities.Game", "Game")
                        .WithMany("Tournaments")
                        .HasForeignKey("GameId")
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.TournamentReference", b =>
                {
                    b.HasOne("Shared.DataAccess.DataBaseEntities.Bot", "Bot")
                        .WithMany("TournamentReference")
                        .HasForeignKey("botId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.DataAccess.DataBaseEntities.Tournament", "Tournament")
                        .WithMany("TournamentReference")
                        .HasForeignKey("tournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bot");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.ArchivedMatches", b =>
                {
                    b.Navigation("ArchivedMatchPlayers");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.Bot", b =>
                {
                    b.Navigation("TournamentReference");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.Game", b =>
                {
                    b.Navigation("ArchivedMatches");

                    b.Navigation("Bot");

                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.Player", b =>
                {
                    b.Navigation("ArchivedMatchPlayers");

                    b.Navigation("Bot");

                    b.Navigation("PlayerPointsList");
                });

            modelBuilder.Entity("Shared.DataAccess.DataBaseEntities.Tournament", b =>
                {
                    b.Navigation("ArchivedMatches");

                    b.Navigation("TournamentReference");
                });
#pragma warning restore 612, 618
        }
    }
}
