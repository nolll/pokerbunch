using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Matrix;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories{

	class MatrixPageModelFactory : IMatrixPageModelFactory
    {
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IPagePropertiesFactory _pagePropertiesFactory;
	    private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;

	    public MatrixPageModelFactory(
            ICashgameRepository cashgameRepository,
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory)
	    {
	        _cashgameRepository = cashgameRepository;
	        _pagePropertiesFactory = pagePropertiesFactory;
	        _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
	    }

	    public CashgameMatrixPageModel Create(Homegame homegame, User user, int? year){
			var suite = _cashgameRepository.GetSuite(homegame, year);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			return new CashgameMatrixPageModel
			    {
			        BrowserTitle = "Cashgame Matrix",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
	                TableModel = new CashgameMatrixTableModel(homegame, suite),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, "matrix", years, year, runningGame)
			    };
		}

	}

}