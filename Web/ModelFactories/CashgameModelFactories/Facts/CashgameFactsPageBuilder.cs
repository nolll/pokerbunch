using Application.Services;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameFacts;
using Core.Repositories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Facts;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.Facts
{
    public class CashgameFactsPageBuilder : ICashgameFactsPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IGlobalization _globalization;
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameContextInteractor _cashgameContextInteractor;
        private readonly ICashgameFactsInteractor _cashgameFactsInteractor;

        public CashgameFactsPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IGlobalization globalization,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory,
            IHomegameRepository homegameRepository,
            ICashgameContextInteractor cashgameContextInteractor,
            ICashgameFactsInteractor cashgameFactsInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _globalization = globalization;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
            _homegameRepository = homegameRepository;
            _cashgameContextInteractor = cashgameContextInteractor;
            _cashgameFactsInteractor = cashgameFactsInteractor;
        }

        public CashgameFactsPageModel Build(string slug, int? year = null)
        {
            var contextResult = GetCashgameContextResult(slug, year);
            var factsResult = GetFactsResult(slug, year);
            var homegame = _homegameRepository.GetBySlug(slug);
            
            var pageProperties = _pagePropertiesFactory.Create(contextResult);
            var pageNavModel = _cashgamePageNavigationModelFactory.Create(contextResult, CashgamePage.Toplist);
            var yearNavModel = _cashgameYearNavigationModelFactory.Create(contextResult, CashgamePage.Toplist);

            return new CashgameFactsPageModel
                {
                    BrowserTitle = "Cashgame Facts",
                    PageProperties = pageProperties,
                    PageNavModel = pageNavModel,
                    YearNavModel = yearNavModel,
			        GameCount = factsResult.GameCount,
			        TotalGameTime = _globalization.FormatDuration(factsResult.MinutesPlayed),
                    TotalTurnover = _globalization.FormatCurrency(homegame.Currency, factsResult.Turnover),
                    BestResultName = factsResult.BestResult.PlayerName,
                    BestResultAmount = _globalization.FormatResult(homegame.Currency, factsResult.BestResult.Amount),
                    WorstResultName = factsResult.WorstResult.PlayerName,
                    WorstResultAmount = _globalization.FormatResult(homegame.Currency, factsResult.WorstResult.Amount),
                    BestTotalWinningsName = factsResult.BestTotalResult.PlayerName,
                    BestTotalWinningsAmount = _globalization.FormatResult(homegame.Currency, factsResult.BestTotalResult.Amount),
                    WorstTotalWinningsName = factsResult.WorstTotalResult.PlayerName,
                    WorstTotalWinningsAmount = _globalization.FormatCurrency(homegame.Currency, factsResult.WorstTotalResult.Amount),
                    MostTimeName = factsResult.MostTimeResult.PlayerName,
                    MostTimeDuration = _globalization.FormatDuration(factsResult.MostTimeResult.Minutes),
                    BiggestTotalBuyinName = factsResult.BiggestBuyinTotalResult.PlayerName,
                    BiggestTotalBuyinAmount = _globalization.FormatCurrency(homegame.Currency, factsResult.BiggestBuyinTotalResult.Amount),
                    BiggestTotalCashoutName = factsResult.BiggestCashoutTotalResult.PlayerName,
                    BiggestTotalCashoutAmount = _globalization.FormatCurrency(homegame.Currency, factsResult.BiggestCashoutTotalResult.Amount)
                };
        }

        private CashgameFactsResult GetFactsResult(string slug, int? year)
        {
            var topListRequest = new CashgameFactsRequest
            {
                Slug = slug,
                Year = year
            };
            return _cashgameFactsInteractor.Execute(topListRequest);
        }

        private CashgameContextResult GetCashgameContextResult(string slug, int? year)
        {
            var contextRequest = new CashgameContextRequest
            {
                Slug = slug,
                Year = year
            };
            return _cashgameContextInteractor.Execute(contextRequest);
        }
    }
}