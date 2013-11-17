using Core.Classes;
using Web.Models.CashgameModels.Cashout;

namespace Web.ModelFactories.CashgameModelFactories.Cashout
{
    public interface ICashoutPageModelFactory
    {
        CashoutPageModel Create(User user, Homegame homegame);
        CashoutPageModel Create(User user, Homegame homegame, CashoutPostModel postModel);
    }
}