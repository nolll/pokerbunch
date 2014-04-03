using System.Collections.Generic;
using Application.Services;
using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Facts;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.Facts
{
    public class CashgameFactsPageModelFactory : ICashgameFactsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IGlobalization _globalization;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;

        public CashgameFactsPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IGlobalization globalization,
            IPlayerRepository playerRepository,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _globalization = globalization;
            _playerRepository = playerRepository;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
        }

        public CashgameFactsPageModel Create(Homegame homegame, CashgameFacts facts, IList<int> years = null, int? year = null)
        {
            var model = new CashgameFactsPageModel
                {
                    BrowserTitle = "Cashgame Facts",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
			        GameCount = facts.GameCount,
			        TotalGameTime = _globalization.FormatDuration(facts.TotalGameTime),
                    TotalTurnover = _globalization.FormatCurrency(homegame.Currency, facts.TotalTurnover),
                    PageNavModel = _cashgamePageNavigationModelFactory.Create(homegame.Slug, CashgamePage.Facts),
                    YearNavModel = _cashgameYearNavigationModelFactory.Create(homegame, years, CashgamePage.Facts, year)
                };

            if (facts.BestResult != null)
            {
                model.BestResultAmount = _globalization.FormatResult(homegame.Currency, facts.BestResult.Winnings);
                var player = _playerRepository.GetById(facts.BestResult.PlayerId);
                if (player != null)
                {
                    model.BestResultName = player.DisplayName;
                }
            }

            if (facts.WorstResult != null)
            {
                model.WorstResultAmount = _globalization.FormatResult(homegame.Currency, facts.WorstResult.Winnings);
                var player = _playerRepository.GetById(facts.WorstResult.PlayerId);
                if (player != null)
                {
                    model.WorstResultName = player.DisplayName;
                }
            }

            if (facts.BestTotalResult != null)
            {
                model.BestTotalWinningsAmount = _globalization.FormatCurrency(homegame.Currency, facts.BestTotalResult.Winnings);
                var player = _playerRepository.GetById(facts.BestTotalResult.PlayerId);
                if (player != null)
                {
                    model.BestTotalWinningsName = player.DisplayName;
                }
            }

            if (facts.WorstTotalResult != null)
            {
                model.WorstTotalWinningsAmount = _globalization.FormatCurrency(homegame.Currency, facts.WorstTotalResult.Winnings);
                var player = _playerRepository.GetById(facts.WorstTotalResult.PlayerId);
                if (player != null)
                {
                    model.WorstTotalWinningsName = player.DisplayName;
                }
            }

            if (facts.MostTimeResult != null)
            {
                model.MostTimeDuration = _globalization.FormatDuration(facts.MostTimeResult.TimePlayed);
                var player = _playerRepository.GetById(facts.MostTimeResult.PlayerId);
                if (player != null)
                {
                    model.MostTimeName = player.DisplayName;
                }
            }

            if (facts.BiggestBuyinTotalResult != null)
            {
                model.BiggestTotalBuyinAmount = _globalization.FormatCurrency(homegame.Currency, facts.BiggestBuyinTotalResult.Buyin);
                var player = _playerRepository.GetById(facts.BiggestBuyinTotalResult.PlayerId);
                if (player != null)
                {
                    model.BiggestTotalBuyinName = player.DisplayName;
                }
            }

            if (facts.BiggestCashoutTotalResult != null)
            {
                model.BiggestTotalCashoutAmount = _globalization.FormatCurrency(homegame.Currency, facts.BiggestCashoutTotalResult.Cashout);
                var player = _playerRepository.GetById(facts.BiggestCashoutTotalResult.PlayerId);
                if (player != null)
                {
                    model.BiggestTotalCashoutName = player.DisplayName;
                }
            }

            return model;
        }

    }

}