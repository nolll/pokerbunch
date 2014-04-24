using Application.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface ICashgameToplistTableModelFactory
    {
        CashgameToplistTableModel Create(CashgameTopListResult topListResult);
    }
}