using Core.Classes;
using Core.Services;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class BarModelFactory : IBarModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public BarModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public BarModel Create(Homegame homegame, Cashgame runningGame)
        {
            var gameIsRunning = runningGame != null;

            return new BarModel
                {
                    GameIsRunning = gameIsRunning,
                    Url = gameIsRunning ? _urlProvider.GetRunningCashgameUrl(homegame) : _urlProvider.GetCashgameAddUrl(homegame)
                };
        }
    }
}