using AutoMapper;
using BotWars.TournamentData;

namespace BotWars;

public class GlobalMappingProfile : Profile
{
    public GlobalMappingProfile()
    {
        CreateMap<TournamentDTO, Tournament>();
        CreateMap<Tournament, TournamentDTO>();
    }
}