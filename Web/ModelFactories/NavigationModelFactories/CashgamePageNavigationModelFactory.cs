using Core.Classes;
using Core.Services;
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

            return new CashgamePageNavigationModel
                {
                    Selected = cashgamePage,
                    MatrixLink = _urlProvider.GetCashgameMatrixUrl(slug, year),
                    MatrixSelectedClass = GetSelectedClass(CashgamePage.Matrix, cashgamePage),
                    ToplistLink = _urlProvider.GetCashgameToplistUrl(slug, year),
                    ToplistSelectedClass = GetSelectedClass(CashgamePage.Toplist, cashgamePage),
                    ChartLink = _urlProvider.GetCashgameChartUrl(slug, year),
                    ChartSelectedClass = GetSelectedClass(CashgamePage.Chart, cashgamePage),
                    ListLink = _urlProvider.GetCashgameListUrl(slug, year),
                    ListSelectedClass = GetSelectedClass(CashgamePage.List, cashgamePage),
                    FactsLink = _urlProvider.GetCashgameFactsUrl(slug, year),
                    FactsSelectedClass = GetSelectedClass(CashgamePage.Facts, cashgamePage)
                };
        }

        private string GetSelectedClass(CashgamePage current, CashgamePage selected)
        {
            return current.Equals(selected) ? "selected" : null;
        }
    }
}