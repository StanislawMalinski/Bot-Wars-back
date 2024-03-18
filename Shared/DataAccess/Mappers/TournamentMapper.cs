using Microsoft.AspNetCore.Http;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
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
                Image = dto.Image
            };
        }

        private byte[] FileToBytes(IFormFile file)
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            return  stream.ToArray();
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
                RankingType = tournament.RankingType,
                Constraints = tournament.Constraints,
                Image = tournament.Image,
                MatchIds = tournament.Matches?
                    .Select(match => match.Id)
                    .ToList(),
                BotIds = tournament.TournamentReference?
                    .Select(reference => reference.botId)
                    .ToList(),
                WasPlayedOut = tournament.Status == TournamentStatus.DONE 
                               
            };
        }

        public Tournament TournamentRequestToTournament(TournamentRequest tournamentRequest)
        {
            return new Tournament()
            {
                TournamentTitle = tournamentRequest.TournamentTitle,
                Description = tournamentRequest.Description,
                GameId = tournamentRequest.GameId,
                PlayersLimit = tournamentRequest.PlayersLimit,
                PostedDate = DateTime.Now,
                TournamentsDate = tournamentRequest.TournamentsDate,
                Status = TournamentStatus.SCHEDULED,
                Constraints = tournamentRequest.Constraints,
                Image = FileToBytes(tournamentRequest.Image),
            };
        }
    }
}
