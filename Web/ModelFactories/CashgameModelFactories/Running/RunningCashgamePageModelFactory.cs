using System;
using Application.Services;
using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Running;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgamePageModelFactory : IRunningCashgamePageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IRunningCashgameTableModelFactory _runningCashgameTableModelFactory;
        private readonly IGlobalization _globalization;

        public RunningCashgamePageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IRunningCashgameTableModelFactory runningCashgameTableModelFactory,
            IGlobalization globalization)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _runningCashgameTableModelFactory = runningCashgameTableModelFactory;
            _globalization = globalization;
        }

        public RunningCashgamePageModel Create(Homegame homegame, Cashgame cashgame, Player player, bool isManager)
        {
            var canBeEnded = CanBeEnded(cashgame);
            var canReport = !canBeEnded;
            var isInGame = cashgame.IsInGame(player.Id);
            
            return new RunningCashgamePageModel
                {
                    BrowserTitle = "Running Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    Location = cashgame.Location,
                    ShowStartTime = cashgame.IsStarted,
                    StartTime = GetStartTime(cashgame, homegame.Timezone),
                    BuyinUrl = new CashgameBuyinUrl(homegame.Slug, player.Id),
                    ReportUrl = new CashgameReportUrl(homegame.Slug, player.Id),
                    CashoutUrl = new CashgameCashoutUrl(homegame.Slug, player.Id),
                    EndGameUrl = new EndCashgameUrl(homegame.Slug),
                    BuyinButtonEnabled = canReport,
                    ReportButtonEnabled = canReport && isInGame,
                    CashoutButtonEnabled = isInGame,
                    EndGameButtonEnabled = canBeEnded,
                    ShowTable = cashgame.IsStarted,
                    RunningCashgameTableModel = cashgame.IsStarted ? _runningCashgameTableModelFactory.Create(homegame, cashgame, isManager) : null,
                    ShowChart = cashgame.IsStarted,
                    ChartDataUrl = GetChartDataUrl(homegame, cashgame)
                };
        }

        private static Url GetChartDataUrl(Homegame homegame, Cashgame cashgame)
        {
            if (cashgame.IsStarted)
                return new CashgameDetailsChartJsonUrl(homegame.Slug, cashgame.DateString);
            return new EmptyUrl();
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