using Core.Classes;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public interface ICashgamePageNavigationModelFactory
    {
        CashgamePageNavigationModel Create(Homegame homegame, CashgamePage cashgamePage, int? year = null);
    }
}