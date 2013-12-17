using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public interface ICashgamePageNavigationModelFactory
    {
        CashgamePageNavigationModel Create(string slug, CashgamePage cashgamePage);
    }
}