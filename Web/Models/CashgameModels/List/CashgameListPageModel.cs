using Application.UseCases.CashgameContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListPageModel : CashgamePageModel
    {
        public CashgameListTableModel ListTableModel { get; set; }

        public CashgameListPageModel(CashgameContextResult cashgameContextResult)
            : base("Cashgame List", cashgameContextResult)
        {
        }
    }
}