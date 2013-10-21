using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class CashgameNavigationModelFactory : ICashgameNavigationModelFactory
    {
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;

        public CashgameNavigationModelFactory(
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory)
        {
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
        }

        public CashgameNavigationModel Create(Homegame homegame, string view, IList<int> years, int? year, Cashgame runningGame)
        {
            return new CashgameNavigationModel
                {
                    PageNavModel = _cashgamePageNavigationModelFactory.Create(homegame, year, view, runningGame),
                    YearNavModel = _cashgameYearNavigationModelFactory.Create(homegame, years, year, view)
                };
        }
    }
}