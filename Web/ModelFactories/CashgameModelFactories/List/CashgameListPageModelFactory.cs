using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListPageModelFactory : ICashgameListPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;
        private readonly ICashgameListTableModelFactory _cashgameListTableModelFactory;

        public CashgameListPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory,
            ICashgameListTableModelFactory cashgameListTableModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
            _cashgameListTableModelFactory = cashgameListTableModelFactory;
        }

        public CashgameListPageModel Create(User user, Homegame homegame, IList<Cashgame> cashgames, IList<int> years, int? year)
        {
            return new CashgameListPageModel
                {
                    BrowserTitle = "Cashgame List",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
			        ListTableModel = _cashgameListTableModelFactory.Create(homegame, cashgames),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, "list", years, year)
                };
        }
    }
}