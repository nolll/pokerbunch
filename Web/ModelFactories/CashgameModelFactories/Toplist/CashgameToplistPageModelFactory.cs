using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public class CashgameToplistPageModelFactory : ICashgameToplistPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;
        private readonly ICashgameToplistTableModelFactory _cashgameToplistTableModelFactory;

        public CashgameToplistPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory,
            ICashgameToplistTableModelFactory cashgameToplistTableModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
            _cashgameToplistTableModelFactory = cashgameToplistTableModelFactory;
        }

        public CashgameToplistPageModel Create(User user, Homegame homegame, CashgameSuite suite, IList<int> years, ToplistSortOrder sortOrder, int? year)
        {
            return new CashgameToplistPageModel
                {
                    BrowserTitle = "Cashgame Toplist",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
			        TableModel = _cashgameToplistTableModelFactory.Create(homegame, suite, year, sortOrder),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, "toplist", years, year)
                };
        }

    }
}