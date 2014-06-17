using Core.Entities;
using Web.Models.CashgameModels.Cashout;

namespace Web.ModelFactories.CashgameModelFactories.Cashout
{
    public interface ICashoutPageBuilder
    {
        CashoutPageModel Build(Homegame homegame, CashoutPostModel postModel);
    }
}