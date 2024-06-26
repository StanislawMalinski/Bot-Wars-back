﻿namespace Shared.DataAccess.DTO.Requests;

public class PlayerInfo
{
    public string? Login { get; set; }
    public long Id { get; set; }
    public long Point { get; set; }
    public DateTime Registered { get; set; }
    public DateTime LastVisit { get; set; }
    public int BotsNumber { get; set; }
    public int TournamentNumber { get; set; }
}