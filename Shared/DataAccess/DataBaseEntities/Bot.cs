﻿using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.DataBaseEntities;

public class Bot
{
    public long Id { get; set; }
    public long PlayerId { get; set; }
    public Player? Player { get; set; }
    public long GameId { get; set; }
    public BotStatus Validation { get; set; }
    public Game? Games { get; set; }
    public string BotFile { get; set; }
    public long FileId { get; set; }
    public int MemoryUsed { get; set; }
    public int TimeUsed { get; set; }
    public Language Language { get; set; }
    public List<TournamentReference>? TournamentReference { get; set; }
    public List<MatchPlayers>? MatchPlayers { get; set; }
}