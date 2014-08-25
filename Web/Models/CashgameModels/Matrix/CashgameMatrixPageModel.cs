using Application.UseCases.CashgameContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixPageModel : CashgamePageModel
    {
        public CashgameMatrixTableModel TableModel { get; set; }

        public CashgameMatrixPageModel(CashgameContextResult cashgameContextResult)
            : base("Cashgame Matrix", cashgameContextResult)
        {
        }
    }
}