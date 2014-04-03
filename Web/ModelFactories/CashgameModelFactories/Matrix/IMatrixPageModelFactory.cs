using Core.Classes;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public interface IMatrixPageModelFactory
    {
		CashgameMatrixPageModel Create(Homegame homegame, int? year);
	}
}