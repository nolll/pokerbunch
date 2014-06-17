using Core.Entities;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public interface IMatrixPageBuilder
    {
		CashgameMatrixPageModel Build(Homegame homegame, int? year);
	}
}