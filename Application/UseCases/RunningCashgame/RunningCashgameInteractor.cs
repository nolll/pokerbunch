using System;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.RunningCashgame
{
    public class RunningCashgameInteractor
    {
        public static RunningCashgameResult Execute(
            IAuth auth,
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            RunningCashgameRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetRunning(bunch);
            var user = auth.CurrentUser;
            var player = playerRepository.GetByUserName(bunch, user.UserName);

            var canBeEnded = CanBeEnded(cashgame);
            var canReport = !canBeEnded;
            var isInGame = cashgame.IsInGame(player.Id);

            var location = cashgame.Location;
            var buyinUrl = new CashgameBuyinUrl(bunch.Slug, player.Id);
            var reportUrl = new CashgameReportUrl(bunch.Slug, player.Id);
            var cashoutUrl = new CashgameCashoutUrl(bunch.Slug, player.Id);
            var endGameUrl = new EndCashgameUrl(bunch.Slug);
            var showStartTime = cashgame.IsStarted;
            var startTime = GetStartTime(cashgame, bunch.Timezone);
            var buyinButtonEnabled = canReport;
            var reportButtonEnabled = canReport && isInGame;
            var cashoutButtonEnabled = isInGame;
            var endGameButtonEnabled = canBeEnded;
            var showTable = cashgame.IsStarted;
            var showChart = cashgame.IsStarted;
            var chartDataUrl = GetChartDataUrl(bunch, cashgame);

            return new RunningCashgameResult(
                location,
                buyinUrl,
                reportUrl,
                cashoutUrl,
                endGameUrl,
                showStartTime,
                startTime,
                buyinButtonEnabled,
                reportButtonEnabled,
                cashoutButtonEnabled,
                endGameButtonEnabled,
                showTable,
                showChart,
                chartDataUrl);
        }

        private static Url GetChartDataUrl(Bunch bunch, Cashgame cashgame)
        {
            if (cashgame.IsStarted)
                return new CashgameDetailsChartJsonUrl(bunch.Slug, cashgame.DateString);
            return Url.Empty;
        }

        private static string GetStartTime(Cashgame cashgame, TimeZoneInfo timezone)
        {
            if (cashgame.IsStarted && cashgame.StartTime.HasValue)
            {
                var localTime = TimeZoneInfo.ConvertTime(cashgame.StartTime.Value, timezone);
                return Globalization.FormatTime(localTime);
            }
            return null;
        }

        private static bool CanBeEnded(Cashgame cashgame)
        {
            return cashgame.IsStarted && !cashgame.HasActivePlayers;
        }
    }

    public class RunningCashgameRequest
    {
        public string Slug { get; private set; }

        public RunningCashgameRequest(string slug)
        {
            Slug = slug;
        }
    }

    public class RunningCashgameResult
    {
        public string Location { get; private set; }
        public Url BuyinUrl { get; private set; }
        public Url ReportUrl { get; private set; }
        public Url CashoutUrl { get; private set; }
        public Url EndGameUrl { get; private set; }
        public bool ShowStartTime { get; private set; }
        public string StartTime { get; private set; }
        public bool BuyinButtonEnabled { get; private set; }
        public bool ReportButtonEnabled { get; private set; }
        public bool CashoutButtonEnabled { get; private set; }
        public bool EndGameButtonEnabled { get; private set; }
        public bool ShowTable { get; private set; }
        public bool ShowChart { get; private set; }
        public Url ChartDataUrl { get; private set; }

        public RunningCashgameResult(
            string location,
            Url buyinUrl,
            Url reportUrl,
            Url cashoutUrl,
            Url endGameUrl,
            bool showStartTime,
            string startTime,
            bool buyinButtonEnabled,
            bool reportButtonEnabled,
            bool cashoutButtonEnabled,
            bool endGameButtonEnabled,
            bool showTable,
            bool showChart,
            Url chartDataUrl)
        {
            Location = location;
            BuyinUrl = buyinUrl;
            ReportUrl = reportUrl;
            CashoutUrl = cashoutUrl;
            EndGameUrl = endGameUrl;
            ShowStartTime = showStartTime;
            StartTime = startTime;
            BuyinButtonEnabled = buyinButtonEnabled;
            ReportButtonEnabled = reportButtonEnabled;
            CashoutButtonEnabled = cashoutButtonEnabled;
            EndGameButtonEnabled = endGameButtonEnabled;
            ShowTable = showTable;
            ShowChart = showChart;
            ChartDataUrl = chartDataUrl;
        }
    }
}
