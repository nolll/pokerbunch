using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Facts;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameFactsPageModelFactory : ICashgameFactsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;

        public CashgameFactsPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
        }

        public CashgameFactsPageModel Create(User user, Homegame homegame, CashgameSuite suite, IList<int> years = null, int? year = null, Cashgame runningGame = null)
        {
            var model = new CashgameFactsPageModel
                {
                    BrowserTitle = "Cashgame Facts",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
			        GameCount = suite.GameCount,
			        TotalGameTime = Globalization.FormatDuration(suite.TotalGameTime),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, "facts", years, year, runningGame)
                };

            if (suite.BestResult != null)
            {
                model.BestResultAmount = Globalization.FormatResult(homegame.Currency, suite.BestResult.Winnings);
                if (suite.BestResult.Player != null)
                {
                    model.BestResultName = suite.BestResult.Player.DisplayName;
                }
            }

            if (suite.WorstResult != null)
            {
                model.WorstResultAmount = Globalization.FormatResult(homegame.Currency, suite.WorstResult.Winnings);
                if (suite.WorstResult.Player != null)
                {
                    model.WorstResultName = suite.WorstResult.Player.DisplayName;
                }
            }

            if (suite.MostTimeResult != null)
            {
                model.MostTimeDuration = Globalization.FormatDuration(suite.MostTimeResult.TimePlayed);
                if (suite.MostTimeResult.Player != null)
                {
                    model.MostTimeName = suite.MostTimeResult.Player.DisplayName;
                }
            }

            return model;
        }

    }

}