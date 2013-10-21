using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix{

	class MatrixPageModelFactory : IMatrixPageModelFactory
    {
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IPagePropertiesFactory _pagePropertiesFactory;
	    private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;
	    private readonly ICashgameMatrixTableModelFactory _cashgameMatrixTableModelFactory;

	    public MatrixPageModelFactory(
            ICashgameRepository cashgameRepository,
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory,
            ICashgameMatrixTableModelFactory cashgameMatrixTableModelFactory)
	    {
	        _cashgameRepository = cashgameRepository;
	        _pagePropertiesFactory = pagePropertiesFactory;
	        _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
	        _cashgameMatrixTableModelFactory = cashgameMatrixTableModelFactory;
	    }

	    public CashgameMatrixPageModel Create(Homegame homegame, User user, int? year){
			var suite = _cashgameRepository.GetSuite(homegame, year);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			return new CashgameMatrixPageModel
			    {
			        BrowserTitle = "Cashgame Matrix",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
	                TableModel = _cashgameMatrixTableModelFactory.Create(homegame, suite),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, "matrix", years, year, runningGame)
			    };
		}

	}

}