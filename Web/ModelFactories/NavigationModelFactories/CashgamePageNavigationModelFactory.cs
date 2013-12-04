using Core.Classes;
using Core.Services;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class CashgamePageNavigationModelFactory : ICashgamePageNavigationModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public CashgamePageNavigationModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public CashgamePageNavigationModel Create(Homegame homegame, CashgamePage cashgamePage, int? year)
        {
            return new CashgamePageNavigationModel
                {
                    Selected = cashgamePage,
                    MatrixLink = _urlProvider.GetCashgameMatrixUrl(homegame, year),
                    MatrixSelectedClass = GetSelectedClass(CashgamePage.Matrix, cashgamePage),
                    ToplistLink = _urlProvider.GetCashgameToplistUrl(homegame, year),
                    ToplistSelectedClass = GetSelectedClass(CashgamePage.Toplist, cashgamePage),
                    ChartLink = _urlProvider.GetCashgameChartUrl(homegame, year),
                    ChartSelectedClass = GetSelectedClass(CashgamePage.Chart, cashgamePage),
                    ListLink = _urlProvider.GetCashgameListUrl(homegame, year),
                    ListSelectedClass = GetSelectedClass(CashgamePage.List, cashgamePage),
                    FactsLink = _urlProvider.GetCashgameFactsUrl(homegame, year),
                    FactsSelectedClass = GetSelectedClass(CashgamePage.Facts, cashgamePage)
                };
        }

        private string GetSelectedClass(CashgamePage current, CashgamePage selected)
        {
            return current.Equals(selected) ? "selected" : null;
        }
    }
}