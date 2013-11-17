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

        public CashgamePageNavigationModel Create(Homegame homegame, int? year = null, string view = null)
        {
            return new CashgamePageNavigationModel
                {
                    Selected = view,
                    MatrixLink = _urlProvider.GetCashgameMatrixUrl(homegame, year),
                    LeaderboardLink = _urlProvider.GetCashgameLeaderboardUrl(homegame, year),
			        ChartLink = _urlProvider.GetCashgameChartUrl(homegame, year),
                    ListingLink = _urlProvider.GetCashgameListingUrl(homegame, year),
                    FactsLink = _urlProvider.GetCashgameFactsUrl(homegame, year)
                };
        }
    }
}