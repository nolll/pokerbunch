using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Matrix;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories{

	class MatrixPageModelFactory : IMatrixPageModelFactory
    {
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IPagePropertiesFactory _pagePropertiesFactory;

	    public MatrixPageModelFactory(
            ICashgameRepository cashgameRepository,
            IPagePropertiesFactory pagePropertiesFactory)
	    {
	        _cashgameRepository = cashgameRepository;
	        _pagePropertiesFactory = pagePropertiesFactory;
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
			        CashgameNavModel = new CashgameNavigationModel(homegame, "matrix", years, year, runningGame)
			    };
		}

	}

}