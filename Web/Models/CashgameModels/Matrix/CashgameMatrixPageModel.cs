using Application.UseCases.CashgameContext;
using Application.UseCases.Matrix;
using Core.Entities;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixPageModel : CashgamePageModel
    {
        public CashgameMatrixTableModel TableModel { get; private set; }

        public CashgameMatrixPageModel(CashgameContextResult cashgameContextResult, MatrixResult matrixResult, Bunch bunch, CashgameSuite suite)
            : base("Cashgame Matrix", cashgameContextResult)
        {
            TableModel = new CashgameMatrixTableModel(matrixResult, bunch, suite);
        }
    }
}