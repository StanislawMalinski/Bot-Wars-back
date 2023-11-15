namespace BotWars.TournamentData
{
    public interface ITournamentMapper
    {
        public Tournament DtoToTournament(TournamentDTO dto);
        public TournamentDTO TournamentToDTO(Tournament tournament);
    }
}
