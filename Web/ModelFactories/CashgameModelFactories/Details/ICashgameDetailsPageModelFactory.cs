using Core.Classes;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public interface ICashgameDetailsPageModelFactory
    {
        CashgameDetailsPageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, bool isManager);
    }
}