using Core.Entities;
using Web.Models.CashgameModels.Running;
using Web.Models.UrlModels;
using Web.Services;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class BarModelFactory : IBarModelFactory
    {
        public BarModel Create(Homegame homegame, Cashgame runningGame)
        {
            var gameIsRunning = runningGame != null;

            return new BarModel
                {
                    GameIsRunning = gameIsRunning,
                    Url = GetUrl(homegame.Slug, gameIsRunning)
                };
        }

        private static Url GetUrl(string slug, bool gameIsRunning)
        {
            if (gameIsRunning)
                return new RunningCashgameUrl(slug);
            return new AddCashgameUrl(slug);
        }
    }
}