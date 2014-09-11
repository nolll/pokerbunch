using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameList;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListPageModel : CashgamePageModel
    {
        public CashgameListTableModel ListTableModel { get; private set; }

        public CashgameListPageModel(CashgameContextResult cashgameContextResult, CashgameListResult listResult)
            : base("Cashgame List", cashgameContextResult)
        {
            ListTableModel = new CashgameListTableModelFactory().Create(listResult);
        }
    }
}