using BotWars.Models;

namespace BotWars.Repository
{
    public class TournamentRepository
    {
        private readonly DataContext _dataContext;
        public TournamentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
