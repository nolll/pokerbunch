using Application.Services;
using Application.UseCases.CashgameContext;
using Core.Services.Interfaces;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class CashgamePageNavigationModelFactory : ICashgamePageNavigationModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly ICashgameService _cashgameService;

        public CashgamePageNavigationModelFactory(
            IUrlProvider urlProvider,
            ICashgameService cashgameService)
        {
            _urlProvider = urlProvider;
            _cashgameService = cashgameService;
        }

        public CashgamePageNavigationModel Create(string slug, CashgamePage cashgamePage)
        {
            var year = _cashgameService.GetLatestYear(slug);
            return Create(slug, year, cashgamePage);
        }

        private CashgamePageNavigationModel Create(string slug, int? latestYear, CashgamePage cashgamePage)
        {
            return new CashgamePageNavigationModel
            {
                Selected = cashgamePage,
                MatrixLink = _urlProvider.GetCashgameMatrixUrl(slug, latestYear),
                MatrixSelectedClass = GetSelectedClass(CashgamePage.Matrix, cashgamePage),
                ToplistLink = _urlProvider.GetCashgameToplistUrl(slug, latestYear),
                ToplistSelectedClass = GetSelectedClass(CashgamePage.Toplist, cashgamePage),
                ChartLink = _urlProvider.GetCashgameChartUrl(slug, latestYear),
                ChartSelectedClass = GetSelectedClass(CashgamePage.Chart, cashgamePage),
                ListLink = _urlProvider.GetCashgameListUrl(slug, latestYear),
                ListSelectedClass = GetSelectedClass(CashgamePage.List, cashgamePage),
                FactsLink = _urlProvider.GetCashgameFactsUrl(slug, latestYear),
                FactsSelectedClass = GetSelectedClass(CashgamePage.Facts, cashgamePage)
            };
        }

        public CashgamePageNavigationModel Create(CashgameContextResult cashgameContextResult, CashgamePage cashgamePage)
        {
            return Create(cashgameContextResult.Slug, cashgameContextResult.LatestYear, cashgamePage);
        }

        private string GetSelectedClass(CashgamePage current, CashgamePage selected)
        {
            return current.Equals(selected) ? "selected" : null;
        }
    }
}