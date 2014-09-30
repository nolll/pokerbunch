using System;
using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Plumbing;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgamePageBuilder : IRunningCashgamePageBuilder
    {
        private readonly IRunningCashgameTableModelFactory _runningCashgameTableModelFactory;
        private readonly IAuth _auth;
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public RunningCashgamePageBuilder(
            IRunningCashgameTableModelFactory runningCashgameTableModelFactory,
            IAuth auth,
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository)
        {
            _runningCashgameTableModelFactory = runningCashgameTableModelFactory;
            _auth = auth;
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
        }

        public RunningCashgamePageModel Build(string slug)
        {
            var user = _auth.CurrentUser;
            var homegame = _bunchRepository.GetBySlug(slug);
            var player = _playerRepository.GetByUserName(homegame, user.UserName);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            var isManager = _auth.IsInRole(slug, Role.Manager);
            
            var canBeEnded = CanBeEnded(cashgame);
            var canReport = !canBeEnded;
            var isInGame = cashgame.IsInGame(player.Id);

            var contextResult = UseCaseContainer.Instance.BunchContext(new BunchContextRequest(slug));

            return new RunningCashgamePageModel(contextResult)
                {
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

        private static Url GetChartDataUrl(Bunch bunch, Cashgame cashgame)
        {
            if (cashgame.IsStarted)
                return new CashgameDetailsChartJsonUrl(bunch.Slug, cashgame.DateString);
            return Url.Empty;
        }

        private string GetStartTime(Cashgame cashgame, TimeZoneInfo timezone)
        {
            if (cashgame.IsStarted && cashgame.StartTime.HasValue)
            {
                var localTime = TimeZoneInfo.ConvertTime(cashgame.StartTime.Value, timezone);
                return Globalization.FormatTime(localTime);
            }
            return null;
        }

        private bool CanBeEnded(Cashgame cashgame)
        {
            return cashgame.IsStarted && !cashgame.HasActivePlayers;
        }
    }
}