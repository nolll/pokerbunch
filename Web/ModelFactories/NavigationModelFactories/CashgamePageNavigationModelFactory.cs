using Core.Classes;
using Core.Services;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class CashgamePageNavigationModelFactory : ICashgamePageNavigationModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public CashgamePageNavigationModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public CashgamePageNavigationModel Create(Homegame homegame, int? year = null, string view = null, Cashgame runningGame = null)
        {
            return new CashgamePageNavigationModel
                {
                    Selected = view,
                    MatrixLink = new CashgameMatrixUrlModel(homegame, year),
			        LeaderboardLink = new CashgameLeaderboardUrlModel(homegame, year),
			        ChartLink = _urlProvider.GetCashgameChartUrl(homegame, year),
			        ListingLink = new CashgameListingUrlModel(homegame, year),
			        FactsLink = new CashgameFactsUrlModel(homegame, year)
                };
        }
    }
}