using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Enumerations;

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
                Image = Convert.FromBase64String(dto.Image)
            };
        }


        public TournamentDto TournamentToDTO(Tournament tournament)
        {
            var dto = new TournamentDto()
            {
                Id = tournament.Id,
                TournamentsTitle = tournament.TournamentTitle,
                Description = tournament.Description,
                GameId = tournament.GameId,
                PlayersLimit = tournament.PlayersLimit,
                PostedDate = tournament.PostedDate,
                TournamentsDate = tournament.TournamentsDate,
                Constrains = tournament.Constraints,
            };
            if (tournament.Image != null)
            {
                dto.Image = Convert.ToBase64String(tournament.Image);
            }

            return dto;
        }

        public TournamentResponse TournamentToTournamentResponse(Tournament tournament)
        {
            var tournamentResponse = new TournamentResponse()
            {
                Id = tournament.Id,
                TournamentTitle = tournament.TournamentTitle,
                Description = tournament.Description,
                PlayersLimit = tournament.PlayersLimit,
                TournamentsDate = tournament.TournamentsDate,
                PostedDate = tournament.PostedDate,
                RankingType = tournament.RankingType,
                Constraints = tournament.Constraints,
                MatchIds = tournament.Matches?
                    .Select(match => match.Id)
                    .ToList(),
                BotIds = tournament.TournamentReference?
                    .Select(reference => reference.botId)
                    .ToList(),
                WasPlayedOut = tournament.Status == TournamentStatus.DONE
            };

            if (tournament.Image != null)
            {
                tournamentResponse.Image = Convert.ToBase64String(tournament.Image);
            }

            return tournamentResponse;
        }

        public Tournament TournamentRequestToTournament(TournamentRequest tournamentRequest)
        {
            var tournament = new Tournament()
            {
                TournamentTitle = tournamentRequest.TournamentTitle,
                Description = tournamentRequest.Description,
                GameId = tournamentRequest.GameId,
                PlayersLimit = tournamentRequest.PlayersLimit,
                PostedDate = DateTime.Now,
                TournamentsDate = tournamentRequest.TournamentsDate,
                Status = TournamentStatus.SCHEDULED,
                Constraints = tournamentRequest.Constraints,
            };

            if (tournamentRequest.Image != null)
            {
                tournament.Image = Convert.FromBase64String(tournamentRequest.Image);
            }

            return tournament;
        }
    }
}