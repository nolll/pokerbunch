using Core.Entities;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public interface ICashgameDetailsPageBuilder
    {
        CashgameDetailsPageModel Build(Homegame homegame, Cashgame cashgame, Player player, bool isManager);
    }
}