using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.Mappers
{
	public class TournamentMapper : ITournamentMapper
    {
        public Tournament DtoToTournament(TournamentDto dto)
        {
            return new Tournament()
            {
                Id = dto.Id,
                TournamentTitle = dto.TournamentsTitle,
                Description = dto.Description,
                GameId = dto.GameId,
                PlayersLimit = dto.PlayersLimit,
                PostedDate = dto.PostedDate,
                TournamentsDate = dto.TournamentsDate,
                //Status = dto.WasPlayedOut,
                Constraints = dto.Constrains,
                Image = dto.Image
            };
        }

        public TournamentDto TournamentToDTO(Tournament tournament)
        {
            return new TournamentDto()
            {
                Id = tournament.Id,
                TournamentsTitle = tournament.TournamentTitle,
                Description = tournament.Description,
                GameId = tournament.GameId,
                PlayersLimit = tournament.PlayersLimit,
                PostedDate = tournament.PostedDate,
                TournamentsDate = tournament.TournamentsDate,
                //WasPlayedOut = tournament.Status,
                Constrains = tournament.Constraints,
                Image = tournament.Image
            };
        }

        public TournamentResponse TournamentToTournamentResponse(Tournament tournament)
        {
            return new TournamentResponse()
            {
                Id = tournament.Id,
                TournamentTitle = tournament.TournamentTitle,
                Description = tournament.Description,
                PlayersLimit = tournament.PlayersLimit,
                TournamentsDate = tournament.TournamentsDate,
                PostedDate = tournament.PostedDate,
                Status = tournament.Status,
                RankingType = tournament.RankingType,
                Constraints = tournament.Constraints,
                Image = tournament.Image,
                MatchIds = tournament.Matches?
                    .Select(match => match.Id)
                    .ToList(),
                BotIds = tournament.TournamentReference
                    .Select(reference => reference.botId)
                    .ToList()
            };
        }
    }
}
