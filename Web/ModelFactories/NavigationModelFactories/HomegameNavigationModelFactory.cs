using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class HomegameNavigationModelFactory : IHomegameNavigationModelFactory
    {
        public HomegameNavigationModel Create(Homegame homegame, Cashgame runningGame)
        {
            return new HomegameNavigationModel
                {
                    Heading = homegame.DisplayName,
			        HeadingLink = new HomegameDetailsUrlModel(homegame),
			        CashgameLink = new CashgameIndexUrlModel(homegame),
			        PlayerLink = new PlayerIndexUrlModel(homegame),
			        CreateLink = new CashgameAddUrlModel(homegame),
			        RunningLink = new RunningCashgameUrlModel(homegame),
			        CashgameIsRunning = runningGame != null
                };
        }
    }
}