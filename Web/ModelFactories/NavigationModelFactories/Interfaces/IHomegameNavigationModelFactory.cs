using Application.UseCases.CashgameContext;
using Core.Classes;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public interface IHomegameNavigationModelFactory
    {
        HomegameNavigationModel Create(Homegame homegame);
        HomegameNavigationModel Create(BunchContextResult bunchContextResult);
    }
}