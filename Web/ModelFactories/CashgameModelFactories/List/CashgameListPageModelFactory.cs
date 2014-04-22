using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.List;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListPageModelFactory : ICashgameListPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameListTableModelFactory _cashgameListTableModelFactory;
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;

        public CashgameListPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameListTableModelFactory cashgameListTableModelFactory,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameListTableModelFactory = cashgameListTableModelFactory;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
        }

        public CashgameListPageModel Create(Homegame homegame, IList<Cashgame> cashgames, IList<int> years, ListSortOrder sortOrder, int? year)
        {
            return new CashgameListPageModel
                {
                    BrowserTitle = "Cashgame List",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
			        ListTableModel = _cashgameListTableModelFactory.Create(homegame, cashgames, sortOrder, year),
                    PageNavModel = _cashgamePageNavigationModelFactory.Create(homegame.Slug, CashgamePage.List),
                    YearNavModel = _cashgameYearNavigationModelFactory.Create(homegame.Slug, years, CashgamePage.List, year)
                };
        }
    }
}