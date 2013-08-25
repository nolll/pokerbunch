using Core.Classes;
using Core.Repositories;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix{

	class MatrixPageModelFactory : IMatrixPageModelFactory
    {
	    private readonly ICashgameRepository _cashgameRepository;

	    public MatrixPageModelFactory(ICashgameRepository cashgameRepository)
	    {
	        _cashgameRepository = cashgameRepository;
	    }

		public MatrixPageModel Create(Homegame homegame, User user, int? year){
			var suite = _cashgameRepository.GetSuite(homegame, year);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			return new MatrixPageModel(user, homegame, suite, years, year, runningGame);
		}

	}

}