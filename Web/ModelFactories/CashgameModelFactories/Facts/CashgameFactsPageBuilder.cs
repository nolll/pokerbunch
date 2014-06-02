using Application.Services;
using Application.UseCases.ApplicationContext;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameFacts;
using Web.Models.CashgameModels.Facts;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.Facts
{
    public class CashgameFactsPageBuilder : ICashgameFactsPageBuilder
    {
        private readonly IGlobalization _globalization;
        private readonly ICashgameContextInteractor _cashgameContextInteractor;
        private readonly IApplicationContextInteractor _applicationContextInteractor;
        private readonly ICashgameFactsInteractor _cashgameFactsInteractor;

        public CashgameFactsPageBuilder(
            IGlobalization globalization,
            ICashgameContextInteractor cashgameContextInteractor,
            IApplicationContextInteractor applicationContextInteractor,
            ICashgameFactsInteractor cashgameFactsInteractor)
        {
            _globalization = globalization;
            _cashgameContextInteractor = cashgameContextInteractor;
            _applicationContextInteractor = applicationContextInteractor;
            _cashgameFactsInteractor = cashgameFactsInteractor;
        }

        public CashgameFactsPageModel Build(string slug, int? year = null)
        {
            var applicationContextResult = _applicationContextInteractor.Execute();
            var cashgameContextResult = _cashgameContextInteractor.Execute(GetCashgameContextRequest(slug, year));
            var factsResult = _cashgameFactsInteractor.Execute(GetFactsRequest(slug, year));

            return new CashgameFactsPageModel(
                applicationContextResult,
                cashgameContextResult)
                {
			        GameCount = factsResult.GameCount,
			        TotalGameTime = _globalization.FormatDuration(factsResult.TimePlayed),
                    TotalTurnover = _globalization.FormatCurrency(factsResult.Turnover),
                    BestResultName = factsResult.BestResult.PlayerName,
                    BestResultAmount = _globalization.FormatResult(factsResult.BestResult.Amount),
                    WorstResultName = factsResult.WorstResult.PlayerName,
                    WorstResultAmount = _globalization.FormatResult(factsResult.WorstResult.Amount),
                    BestTotalWinningsName = factsResult.BestTotalResult.PlayerName,
                    BestTotalWinningsAmount = _globalization.FormatResult(factsResult.BestTotalResult.Amount),
                    WorstTotalWinningsName = factsResult.WorstTotalResult.PlayerName,
                    WorstTotalWinningsAmount = _globalization.FormatCurrency(factsResult.WorstTotalResult.Amount),
                    MostTimeName = factsResult.MostTimeResult.PlayerName,
                    MostTimeDuration = _globalization.FormatDuration(factsResult.MostTimeResult.Minutes),
                    BiggestTotalBuyinName = factsResult.BiggestBuyinTotalResult.PlayerName,
                    BiggestTotalBuyinAmount = _globalization.FormatCurrency(factsResult.BiggestBuyinTotalResult.Amount),
                    BiggestTotalCashoutName = factsResult.BiggestCashoutTotalResult.PlayerName,
                    BiggestTotalCashoutAmount = _globalization.FormatCurrency(factsResult.BiggestCashoutTotalResult.Amount)
                };
        }

        private static CashgameFactsRequest GetFactsRequest(string slug, int? year)
        {
            var topListRequest = new CashgameFactsRequest
                {
                    Slug = slug,
                    Year = year
                };
            return topListRequest;
        }

        private static CashgameContextRequest GetCashgameContextRequest(string slug, int? year)
        {
            var contextRequest = new CashgameContextRequest
                {
                    Slug = slug,
                    Year = year
                };
            return contextRequest;
        }
    }
}