using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public interface ICashgameNavigationModelFactory
    {
        CashgameNavigationModel Create(Homegame homegame, CashgamePage cashgamePage, IList<int> years, int? year);
    }
}