using System.Collections.Generic;
using Application.UseCases.CashgameContext;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class CashgameYearNavigationModelFactory : ICashgameYearNavigationModelFactory
    {
        public CashgameYearNavigationModel Create(string slug, IList<int> years, CashgamePage cashgamePage, int? year)
        {
            return new CashgameYearNavigationModel(slug, years, cashgamePage, year);
        }
    }
}