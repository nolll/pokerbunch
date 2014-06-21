using Application.UseCases.CashgameContext;
using Web.Models.NavigationModels;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListPageModel : CashgameContextPageModel
    {
        public CashgameListTableModel ListTableModel { get; set; }

        public CashgameListPageModel(CashgameContextResult cashgameContextResult, CashgamePage selectedPage)
            : base("Cashgame List", cashgameContextResult, selectedPage)
        {
        }
    }
}