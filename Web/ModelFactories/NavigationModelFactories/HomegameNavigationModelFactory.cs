using Core.Classes;
using Core.Services;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class HomegameNavigationModelFactory : IHomegameNavigationModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public HomegameNavigationModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public HomegameNavigationModel Create(Homegame homegame, Cashgame runningGame)
        {
            return new HomegameNavigationModel
                {
                    Heading = homegame.DisplayName,
			        HeadingLink = _urlProvider.GetHomegameDetailsUrl(homegame),
			        CashgameLink = _urlProvider.GetCashgameIndexUrl(homegame),
                    PlayerLink = _urlProvider.GetPlayerIndexUrl(homegame),
			        CreateLink = _urlProvider.GetCashgameAddUrl(homegame),
                    RunningLink = _urlProvider.GetRunningCashgameUrl(homegame),
			        CashgameIsRunning = runningGame != null
                };
        }
    }
}