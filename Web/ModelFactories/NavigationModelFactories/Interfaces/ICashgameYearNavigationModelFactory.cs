using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public interface ICashgameYearNavigationModelFactory
    {
        CashgameYearNavigationModel Create(Homegame homegame, IList<int> years, int? year = null, string view = null);
    }
}