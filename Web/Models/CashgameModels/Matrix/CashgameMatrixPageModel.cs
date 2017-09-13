using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixPageModel : CashgamePageModel
    {
        public CashgameMatrixTableModel TableModel { get; private set; }

        public CashgameMatrixPageModel(CashgameContext.Result cashgameContextResult, Core.UseCases.Matrix.Result matrixResult)
            : base(cashgameContextResult)
        {
            TableModel = new CashgameMatrixTableModel(matrixResult);
        }

        public override string BrowserTitle => "Cashgame Matrix";
        public override View GetView()
        {
            return new View("~/Views/Pages/Matrix/MatrixPage.cshtml");
        }
    }
}