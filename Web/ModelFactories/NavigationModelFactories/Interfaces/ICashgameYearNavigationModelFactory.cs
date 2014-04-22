using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public interface ICashgameYearNavigationModelFactory
    {
        CashgameYearNavigationModel Create(string slug, IList<int> years, CashgamePage cashgamePage, int? year = null);
    }
}