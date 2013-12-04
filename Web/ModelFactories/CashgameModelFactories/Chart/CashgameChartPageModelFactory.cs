﻿using System.Collections.Generic;
using Core.Classes;
using Core.Services;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Chart;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public class CashgameChartPageModelFactory : ICashgameChartPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;

        public CashgameChartPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
            _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
        }

        public CashgameChartPageModel Create(User user, Homegame homegame, int? year, IList<int> years)
        {
            return new CashgameChartPageModel
                {
                    BrowserTitle = "Cashgame Chart",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
			        ChartDataUrl = _urlProvider.GetCashgameChartJsonUrl(homegame, year),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, CashgamePage.Chart, years, year)
                };
        }
    }
}