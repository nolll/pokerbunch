using System;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgamePageBuilder : IRunningCashgamePageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IRunningCashgameTableModelFactory _runningCashgameTableModelFactory;
        private readonly IGlobalization _globalization;
        private readonly IAuth _auth;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public RunningCashgamePageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IRunningCashgameTableModelFactory runningCashgameTableModelFactory,
            IGlobalization globalization,
            IAuth auth,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _runningCashgameTableModelFactory = runningCashgameTableModelFactory;
            _globalization = globalization;
            _auth = auth;
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
        }

        public RunningCashgamePageModel Build(string slug)
        {
            var user = _auth.CurrentUser;
            var homegame = _homegameRepository.GetBySlug(slug);
            var player = _playerRepository.GetByUserName(homegame, user.UserName);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            var isManager = _auth.IsInRole(slug, Role.Manager);
            
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
            return Url.Empty;
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