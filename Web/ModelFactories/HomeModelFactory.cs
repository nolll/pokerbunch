using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Web.Models;

namespace Web.ModelFactories
{
    public class HomeModelFactory : IHomeModelFactory
    {
        private readonly IUserContext _userContext;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public HomeModelFactory(IUserContext userContext, IHomegameRepository homegameRepository, ICashgameRepository cashgameRepository)
        {
            _userContext = userContext;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public HomeModel Create()
        {
            var homegame = GetHomegame();
            var runningGame = GetRunningGame(homegame);
            var model = new HomeModel(_userContext.GetUser(), homegame, runningGame);
            return model;
        }

        private Homegame GetHomegame()
        {
            var games = _homegameRepository.GetByUser(_userContext.GetUser());
            return games.Count == 1 ? games[0] : null;
        }

        private Cashgame GetRunningGame(Homegame homegame)
        {
            if (homegame == null)
            {
                return null;
            }
            return _cashgameRepository.GetRunning(homegame);
        }
    }
}