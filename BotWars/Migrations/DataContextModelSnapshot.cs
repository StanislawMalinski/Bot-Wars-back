﻿// <auto-generated />
using System;
using BotWars.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BotWars.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BotWars.Gry.ArchivedMatchPlayers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("MatchId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<long>("TournamentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TournamentId")
                        .IsUnique();

                    b.ToTable("ArchivedMatchPlayers");
                });

            modelBuilder.Entity("BotWars.Gry.ArchivedMatches", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("Match")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Played")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("TournamentsId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("TournamentsId");

                    b.ToTable("ArchivedMatches");
                });

            modelBuilder.Entity("BotWars.Gry.Bot", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("BotFile")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Bots");
                });

            modelBuilder.Entity("BotWars.Gry.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("GameFile")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GameInstructions")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("InterfaceDefinition")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAvaiableForPlay")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastModification")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("NumbersOfPlayer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("BotWars.Gry.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("BotWars.Gry.Tournament", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Contrains")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<int>("PlayersLimit")
                        .HasColumnType("int");

                    b.Property<DateTime>("PostedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TournamentTitles")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("TournamentsDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("WasPlayedOut")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("BotWars.Gry.TournamentReference", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("bodId")
                        .HasColumnType("bigint");

                    b.Property<long>("tournamentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("bodId");

                    b.HasIndex("tournamentId");

                    b.ToTable("TournamentReference");
                });

            modelBuilder.Entity("BotWars.Gry.ArchivedMatchPlayers", b =>
                {
                    b.HasOne("BotWars.Gry.ArchivedMatches", "archivedMatches")
                        .WithMany("ArchivedMatchPlayers")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotWars.Gry.Player", "Player")
                        .WithMany("ArchivedMatchPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotWars.Gry.Tournament", "Tournament")
                        .WithOne("ArchivedMatchPlayers")
                        .HasForeignKey("BotWars.Gry.ArchivedMatchPlayers", "TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Tournament");

                    b.Navigation("archivedMatches");
                });

            modelBuilder.Entity("BotWars.Gry.ArchivedMatches", b =>
                {
                    b.HasOne("BotWars.Gry.Game", "Game")
                        .WithMany("ArchivedMatches")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotWars.Gry.Tournament", "Tournament")
                        .WithMany("ArchivedMatches")
                        .HasForeignKey("TournamentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("BotWars.Gry.Bot", b =>
                {
                    b.HasOne("BotWars.Gry.Game", "Games")
                        .WithMany("Bot")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotWars.Gry.Player", "Players")
                        .WithMany("Bot")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Games");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("BotWars.Gry.Tournament", b =>
                {
                    b.HasOne("BotWars.Gry.Game", "Game")
                        .WithMany("Tournaments")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("BotWars.Gry.TournamentReference", b =>
                {
                    b.HasOne("BotWars.Gry.Bot", "Bot")
                        .WithMany("TournamentReference")
                        .HasForeignKey("bodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotWars.Gry.Tournament", "Tournament")
                        .WithMany("TournamentReference")
                        .HasForeignKey("tournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bot");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("BotWars.Gry.ArchivedMatches", b =>
                {
                    b.Navigation("ArchivedMatchPlayers");
                });

            modelBuilder.Entity("BotWars.Gry.Bot", b =>
                {
                    b.Navigation("TournamentReference");
                });

            modelBuilder.Entity("BotWars.Gry.Game", b =>
                {
                    b.Navigation("ArchivedMatches");

                    b.Navigation("Bot");

                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("BotWars.Gry.Player", b =>
                {
                    b.Navigation("ArchivedMatchPlayers");

                    b.Navigation("Bot");
                });

            modelBuilder.Entity("BotWars.Gry.Tournament", b =>
                {
                    b.Navigation("ArchivedMatchPlayers");

                    b.Navigation("ArchivedMatches");

                    b.Navigation("TournamentReference");
                });
#pragma warning restore 612, 618
        }
    }
}
