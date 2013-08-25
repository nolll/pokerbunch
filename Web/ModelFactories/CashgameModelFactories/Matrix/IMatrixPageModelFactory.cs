using Core.Classes;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix{
    public interface IMatrixPageModelFactory {

		MatrixPageModel Create(Homegame homegame, User user, int? year);

	}

}