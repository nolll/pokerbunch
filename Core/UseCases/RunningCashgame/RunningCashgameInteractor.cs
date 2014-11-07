using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.RunningCashgame
{
    public class RunningCashgameInteractor
    {
        public static RunningCashgameResult Execute(
            IAuth auth,
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            ITimeProvider timeProvider,
            RunningCashgameRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetRunning(bunch);

            if(cashgame == null)
                throw new CashgameNotRunningException();

            var user = auth.CurrentUser;
            var player = playerRepository.GetByUserId(bunch.Id, user.Id);
            var players = playerRepository.GetList(GetPlayerIds(cashgame));

            var isStarted = cashgame.IsStarted;
            var canBeEnded = CanBeEnded(cashgame);
            var canReport = !canBeEnded;
            var isInGame = cashgame.IsInGame(player.Id);
            var isManager = auth.IsInRole(request.Slug, Role.Manager);
            var now = timeProvider.UtcNow;
            
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

            var items = GetItems(bunch, cashgame, players, isManager, now);
            var totalBuyin = new Money(cashgame.Turnover, bunch.Currency);
            var totalStacks = new Money(cashgame.TotalStacks, bunch.Currency);

            return new RunningCashgameResult(
                location,
                buyinUrl,
                reportUrl,
                cashoutUrl,
                endGameUrl,
                showStartTime,
                startTime,
                isStarted,
                buyinButtonEnabled,
                reportButtonEnabled,
                cashoutButtonEnabled,
                endGameButtonEnabled,
                showTable,
                showChart,
                chartDataUrl,
                items,
                totalBuyin,
                totalStacks);
        }

        private static IList<int> GetPlayerIds(Cashgame cashgame)
        {
            return cashgame.Results.Select(o => o.PlayerId).ToList();
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

        private static IList<RunningCashgameTableItem> GetItems(Bunch bunch, Cashgame cashgame, IList<Player> players, bool isManager, DateTime now)
        {
            var results = GetSortedResults(cashgame);
            var items = new List<RunningCashgameTableItem>();
            foreach (var result in results)
            {
                var playerId = result.PlayerId;
                var player = players.First(o => o.Id == playerId);

                var name = player.DisplayName;
                var playerUrl = new CashgameActionUrl(bunch.Slug, cashgame.DateString, player.Id);
                var buyinUrl = new CashgameBuyinUrl(bunch.Slug, player.Id);
                var reportUrl = new CashgameReportUrl(bunch.Slug, player.Id);
                var cashoutUrl = new CashgameCashoutUrl(bunch.Slug, player.Id);
                var buyin = new Money(result.Buyin, bunch.Currency);
                var stack = new Money(result.Stack, bunch.Currency);
                var winnings = new Money(result.Winnings, bunch.Currency);
                var time = GetTime(result.LastReportTime, now);
                var hasCashedOut = result.CashoutTime != null;

                var item = new RunningCashgameTableItem(
                    name,
                    playerUrl,
                    buyin,
                    stack,
                    winnings,
                    time,
                    buyinUrl,
                    reportUrl,
                    cashoutUrl,
                    hasCashedOut,
                    isManager);

                items.Add(item);
            }

            return items;
        }

        private static Time GetTime(DateTime? lastReportedTime, DateTime now)
        {
            if (!lastReportedTime.HasValue)
                return null;
            var timespan = now - lastReportedTime.Value;
            return Time.FromTimeSpan(timespan);
        }

        private static IEnumerable<CashgameResult> GetSortedResults(Cashgame cashgame)
        {
            var results = cashgame.Results;
            return results.OrderByDescending(o => o.Winnings);
        }
    }
}
