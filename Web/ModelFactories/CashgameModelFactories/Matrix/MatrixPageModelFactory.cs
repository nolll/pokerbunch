using Application.Services;
using Core.Classes;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Matrix;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
	public class MatrixPageModelFactory : IMatrixPageModelFactory
    {
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly ICashgameService _cashgameService;
	    private readonly IPagePropertiesFactory _pagePropertiesFactory;
	    private readonly ICashgameMatrixTableModelFactory _cashgameMatrixTableModelFactory;
	    private readonly IBarModelFactory _barModelFactory;
	    private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
	    private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;
	    private readonly IUrlProvider _urlProvider;

	    public MatrixPageModelFactory(
            ICashgameRepository cashgameRepository,
            ICashgameService cashgameService,
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameMatrixTableModelFactory cashgameMatrixTableModelFactory,
            IBarModelFactory barModelFactory,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory,
            IUrlProvider urlProvider)
	    {
	        _cashgameRepository = cashgameRepository;
	        _cashgameService = cashgameService;
	        _pagePropertiesFactory = pagePropertiesFactory;
	        _cashgameMatrixTableModelFactory = cashgameMatrixTableModelFactory;
	        _barModelFactory = barModelFactory;
	        _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
	        _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
	        _urlProvider = urlProvider;
	    }

	    public CashgameMatrixPageModel Create(Homegame homegame, int? year){
            var suite = _cashgameService.GetSuite(homegame, year);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
	        var gameIsRunning = runningGame != null;
	        var startGameUrl = !gameIsRunning ? _urlProvider.GetCashgameAddUrl(homegame.Slug) : null;

			return new CashgameMatrixPageModel
			    {
			        BrowserTitle = "Cashgame Matrix",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
	                TableModel = _cashgameMatrixTableModelFactory.Create(homegame, suite),
                    PageNavModel = _cashgamePageNavigationModelFactory.Create(homegame.Slug, CashgamePage.Matrix),
                    YearNavModel = _cashgameYearNavigationModelFactory.Create(homegame, years, CashgamePage.Matrix, year),
                    BarModel = _barModelFactory.Create(homegame, runningGame),
                    GameIsRunning = gameIsRunning,
                    StartGameUrl = startGameUrl
			    };
		}
	}
}