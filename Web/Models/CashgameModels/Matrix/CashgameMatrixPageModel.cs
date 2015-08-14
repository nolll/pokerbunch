using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixPageModel : CashgamePageModel
    {
        public CashgameMatrixTableModel TableModel { get; private set; }

        public CashgameMatrixPageModel(CashgameContext.Result cashgameContextResult, Core.UseCases.Matrix.Result matrixResult)
            : base("Cashgame Matrix", cashgameContextResult)
        {
            TableModel = new CashgameMatrixTableModel(matrixResult);
        }
    }
}