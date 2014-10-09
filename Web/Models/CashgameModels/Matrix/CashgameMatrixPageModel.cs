using Core.UseCases.CashgameContext;
using Core.UseCases.Matrix;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixPageModel : CashgamePageModel
    {
        public CashgameMatrixTableModel TableModel { get; private set; }

        public CashgameMatrixPageModel(CashgameContextResult cashgameContextResult, MatrixResult matrixResult)
            : base("Cashgame Matrix", cashgameContextResult)
        {
            TableModel = new CashgameMatrixTableModel(matrixResult);
        }
    }
}