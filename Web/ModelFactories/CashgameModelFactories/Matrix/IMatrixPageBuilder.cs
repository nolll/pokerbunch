using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public interface IMatrixPageBuilder
    {
		CashgameMatrixPageModel Build(string slug, int? year);
	}
}