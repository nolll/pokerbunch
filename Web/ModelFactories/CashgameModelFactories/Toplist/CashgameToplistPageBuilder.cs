using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Toplist;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public class CashgameToplistPageBuilder : ICashgameToplistPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameToplistTableModelFactory _cashgameToplistTableModelFactory;
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;

        public CashgameToplistPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameToplistTableModelFactory cashgameToplistTableModelFactory,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameToplistTableModelFactory = cashgameToplistTableModelFactory;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
        }

        public CashgameToplistPageModel Create(Homegame homegame, CashgameSuite suite, IList<int> years, ToplistSortOrder sortOrder, int? year)
        {
            return new CashgameToplistPageModel
                {
                    BrowserTitle = "Cashgame Toplist",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
			        TableModel = _cashgameToplistTableModelFactory.Create(homegame, suite, year, sortOrder),
                    PageNavModel = _cashgamePageNavigationModelFactory.Create(homegame.Slug, CashgamePage.Toplist),
                    YearNavModel = _cashgameYearNavigationModelFactory.Create(homegame, years, CashgamePage.Toplist, year)
                };
        }

    }
}