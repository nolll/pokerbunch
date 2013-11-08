using Core.Classes;
using Web.Models.CashgameModels.Cashout;

namespace Web.ModelFactories.CashgameModelFactories.Cashout
{
    public interface ICashoutPageModelFactory
    {
        CashoutPageModel Create(User user, Homegame homegame, Cashgame runningGame);
        CashoutPageModel Create(User user, Homegame homegame, Cashgame runningGame, CashoutPostModel postModel);
    }
}