using Core.Repositories;
using Core.Services;
using Web.ModelFactories.CashgameModelFactories.Leaderboard;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelServices
{
    public class CashgameModelService : ICashgameModelService
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IUserContext _userContext;
        private readonly IMatrixPageModelFactory _matrixPageModelFactory;
        private readonly ICashgameService _cashgameService;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameLeaderboardPageModelFactory _cashgameLeaderboardPageModelFactory;

        public CashgameModelService(
            IHomegameRepository homegameRepository,
            IUserContext userContext,
            IMatrixPageModelFactory matrixPageModelFactory,
            ICashgameService cashgameService,
            ICashgameRepository cashgameRepository,
            ICashgameLeaderboardPageModelFactory cashgameLeaderboardPageModelFactory)
        {
            _homegameRepository = homegameRepository;
            _userContext = userContext;
            _matrixPageModelFactory = matrixPageModelFactory;
            _cashgameService = cashgameService;
            _cashgameRepository = cashgameRepository;
            _cashgameLeaderboardPageModelFactory = cashgameLeaderboardPageModelFactory;
        }

        public CashgameMatrixPageModel GetMatrixModel(string gameName, int? year = null)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequirePlayer(homegame);
            return _matrixPageModelFactory.Create(homegame, _userContext.GetUser(), year);
        }

        public CashgameLeaderboardPageModel GetLeaderboardModel(string gameName, int? year = null)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequirePlayer(homegame);
            var suite = _cashgameService.GetSuite(homegame, year);
            var years = _cashgameRepository.GetYears(homegame);
            return _cashgameLeaderboardPageModelFactory.Create(_userContext.GetUser(), homegame, suite, years, year);
        }

    }
}