using System;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgamePageModelFactory : IRunningCashgamePageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IRunningCashgameTableModelFactory _runningCashgameTableModelFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly IGlobalization _globalization;

        public RunningCashgamePageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IRunningCashgameTableModelFactory runningCashgameTableModelFactory,
            IUrlProvider urlProvider,
            IGlobalization globalization)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _runningCashgameTableModelFactory = runningCashgameTableModelFactory;
            _urlProvider = urlProvider;
            _globalization = globalization;
        }

        public RunningCashgamePageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, bool isManager)
        {
            var canBeEnded = CanBeEnded(cashgame);
            var canReport = !canBeEnded;
            var isInGame = cashgame.IsInGame(player.Id);
            
            return new RunningCashgamePageModel
                {
                    BrowserTitle = "Running Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
                    Location = cashgame.Location,
                    ShowStartTime = cashgame.IsStarted,
                    StartTime = GetStartTime(cashgame, homegame.Timezone),
                    BuyinUrl = _urlProvider.GetCashgameBuyinUrl(homegame, player),
                    ReportUrl = _urlProvider.GetCashgameReportUrl(homegame, player),
                    CashoutUrl = _urlProvider.GetCashgameCashoutUrl(homegame, player),
                    EndGameUrl = _urlProvider.GetCashgameEndUrl(homegame),
                    BuyinButtonEnabled = canReport,
                    ReportButtonEnabled = canReport && isInGame,
                    CashoutButtonEnabled = isInGame,
                    EndGameButtonEnabled = canBeEnded,
                    ShowTable = cashgame.IsStarted,
                    RunningCashgameTableModel = cashgame.IsStarted ? _runningCashgameTableModelFactory.Create(homegame, cashgame, isManager) : null,
                    ShowChart = cashgame.IsStarted,
                    ChartDataUrl = cashgame.IsStarted ? _urlProvider.GetCashgameDetailsChartJsonUrl(homegame, cashgame) : null
                };
        }

        private string GetStartTime(Cashgame cashgame, TimeZoneInfo timezone)
        {
            if (cashgame.IsStarted && cashgame.StartTime.HasValue)
            {
                var localTime = TimeZoneInfo.ConvertTime(cashgame.StartTime.Value, timezone);
                return _globalization.FormatTime(localTime);
            }
            return null;
        }

        private bool CanBeEnded(Cashgame cashgame)
        {
            return cashgame.IsStarted && !cashgame.HasActivePlayers;
        }
    }
}