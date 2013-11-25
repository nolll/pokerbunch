using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Facts;

namespace Web.ModelFactories.CashgameModelFactories.Facts
{
    public class CashgameFactsPageModelFactory : ICashgameFactsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;
        private readonly IGlobalization _globalization;

        public CashgameFactsPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory,
            IGlobalization globalization)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
            _globalization = globalization;
        }

        public CashgameFactsPageModel Create(User user, Homegame homegame, CashgameFacts facts, IList<int> years = null, int? year = null, Cashgame runningGame = null)
        {
            var model = new CashgameFactsPageModel
                {
                    BrowserTitle = "Cashgame Facts",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
			        GameCount = facts.GameCount,
			        TotalGameTime = _globalization.FormatDuration(facts.TotalGameTime),
                    TotalTurnover = _globalization.FormatCurrency(homegame.Currency, facts.TotalTurnover),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, "facts", years, year)
                };

            if (facts.BestResult != null)
            {
                model.BestResultAmount = _globalization.FormatResult(homegame.Currency, facts.BestResult.Winnings);
                if (facts.BestResult.Player != null)
                {
                    model.BestResultName = facts.BestResult.Player.DisplayName;
                }
            }

            if (facts.WorstResult != null)
            {
                model.WorstResultAmount = _globalization.FormatResult(homegame.Currency, facts.WorstResult.Winnings);
                if (facts.WorstResult.Player != null)
                {
                    model.WorstResultName = facts.WorstResult.Player.DisplayName;
                }
            }

            if (facts.MostTimeResult != null)
            {
                model.MostTimeDuration = _globalization.FormatDuration(facts.MostTimeResult.TimePlayed);
                if (facts.MostTimeResult.Player != null)
                {
                    model.MostTimeName = facts.MostTimeResult.Player.DisplayName;
                }
            }

            return model;
        }

    }

}