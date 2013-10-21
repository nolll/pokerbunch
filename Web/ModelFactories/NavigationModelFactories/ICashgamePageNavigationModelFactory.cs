using Core.Classes;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public interface ICashgamePageNavigationModelFactory
    {
        CashgamePageNavigationModel Create(Homegame homegame, int? year = null, string view = null, Cashgame runningGame = null);
    }
}