﻿using BotWars.Models;

namespace BotWars.Gry
{
    public class Game : IFileData
    {
        public long Id { get; set; }
        public String? Description { get; set; }
        public String? Filename { get; set; }
        public byte[]? Data { get; set; }

    }
}
