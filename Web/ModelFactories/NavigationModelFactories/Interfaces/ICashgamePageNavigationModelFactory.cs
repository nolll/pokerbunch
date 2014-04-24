using Application.UseCases.CashgameContext;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public interface ICashgamePageNavigationModelFactory
    {
        CashgamePageNavigationModel Create(string slug, CashgamePage cashgamePage);
        CashgamePageNavigationModel Create(CashgameContextResult cashgameContextResult, CashgamePage cashgamePage);
    }
}