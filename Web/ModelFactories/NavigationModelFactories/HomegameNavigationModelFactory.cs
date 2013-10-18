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
			        HeadingLink = new HomegameDetailsUrlModel(homegame),
			        CashgameLink = new CashgameIndexUrlModel(homegame),
			        PlayerLink = new PlayerIndexUrlModel(homegame),
			        CreateLink = _urlProvider.GetCashgameAddUrl(homegame),
			        RunningLink = new RunningCashgameUrlModel(homegame),
			        CashgameIsRunning = runningGame != null
                };
        }
    }
}