using System.Web.Mvc;
using Core.Repositories;
using Web.ModelFactories;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.UrlModels;

namespace Web.Controllers{

	public class CashgameController : Controller {
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IUserContext _userContext;
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IMatrixPageModelFactory _matrixPageModelFactory;

	    public CashgameController(IHomegameRepository homegameRepository, IUserContext userContext, ICashgameRepository cashgameRepository, IMatrixPageModelFactory matrixPageModelFactory)
	    {
	        _homegameRepository = homegameRepository;
	        _userContext = userContext;
	        _cashgameRepository = cashgameRepository;
	        _matrixPageModelFactory = matrixPageModelFactory;
	    }

	    public ActionResult Index(string gameName){
            var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			if(years.Count > 0){
				var year = years[0];
				return new RedirectResult(new CashgameMatrixUrlModel(homegame, year).Url);
			}
			return new RedirectResult(new CashgameAddUrlModel(homegame).Url);
		}

        public ActionResult Matrix(string gameName, int? year = null){
            var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var model = _matrixPageModelFactory.Create(homegame, _userContext.GetUser(), year);
			return View("Matrix/MatrixPage", model);
		}

        public ActionResult Leaderboard(string gameName, int? year = null){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var suite = _cashgameRepository.GetSuite(homegame, year);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var model = new LeaderboardPageModel(_userContext.GetUser(), homegame, suite, years, year, runningGame);
			return View("Leaderboard/LeaderboardPage", model);
		}

	}

}