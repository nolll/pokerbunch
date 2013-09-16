using Core.Classes;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories{
    public interface IMatrixPageModelFactory {

		CashgameMatrixPageModel Create(Homegame homegame, User user, int? year);

	}

}