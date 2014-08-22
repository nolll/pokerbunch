using Application.UseCases.CashgameContext;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListPageModel : CashgameContextPageModel
    {
        public CashgameListTableModel ListTableModel { get; set; }

        public CashgameListPageModel(CashgameContextResult cashgameContextResult)
            : base("Cashgame List", cashgameContextResult)
        {
        }
    }
}