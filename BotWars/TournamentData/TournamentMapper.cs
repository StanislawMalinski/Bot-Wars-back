namespace BotWars.TournamentData
{
    public class TournamentMapper : ITournamentMapper
    {
        public Tournament DtoToTournament(TournamentDTO dto)
        {
            return new Tournament()
            {
                Id = dto.Id,
                TournamentTitles = dto.TournamentsTitle,
                Description = dto.Description,
                GameId = dto.GameId,
                PlayersLimit = dto.PlayersLimit,
                PostedDate = dto.PostedDate,
                TournamentsDate = dto.TournamentsDate,
                WasPlayedOut = dto.WasPlayedOut,
                Contrains = dto.Contrains,
                Image = dto.Image
            };
        }

        public TournamentDTO TournamentToDTO(Tournament tournament)
        {
            return new TournamentDTO()
            {
                Id = tournament.Id,
                TournamentsTitle = tournament.TournamentTitles,
                Description = tournament.Description,
                GameId = tournament.GameId,
                PlayersLimit = tournament.PlayersLimit,
                PostedDate = tournament.PostedDate,
                TournamentsDate = tournament.TournamentsDate,
                WasPlayedOut = tournament.WasPlayedOut,
                Contrains = tournament.Contrains,
                Image = tournament.Image
            };
        }
    }
}
