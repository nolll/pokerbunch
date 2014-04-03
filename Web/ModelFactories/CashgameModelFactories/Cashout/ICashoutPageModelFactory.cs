using Core.Classes;
using Web.Models.CashgameModels.Cashout;

namespace Web.ModelFactories.CashgameModelFactories.Cashout
{
    public interface ICashoutPageModelFactory
    {
        CashoutPageModel Create(Homegame homegame, CashoutPostModel postModel);
    }
}