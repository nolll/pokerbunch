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
            var cashgame = cashgameRepository.GetRunning(bunch.Id);

            if(cashgame == null)
                throw new CashgameNotRunningException();

            var player = playerRepository.GetByUserId(bunch.Id, auth.CurrentIdentity.UserId);
            var players = playerRepository.GetList(GetPlayerIds(cashgame));
            var bunchPlayers = playerRepository.GetList(bunch.Id);

            var isStarted = cashgame.IsStarted;
            var isManager = auth.IsInRole(request.Slug, Role.Manager);
            var now = timeProvider.UtcNow;
            
            var location = cashgame.Location;
            var gameDataUrl = new RunningCashgameGameJsonUrl(bunch.Slug);
            var playerDataUrl = new RunningCashgamePlayersJsonUrl(bunch.Slug);
            var buyinUrl = new CashgameBuyinUrl(bunch.Slug);
            var reportUrl = new CashgameReportUrl(bunch.Slug);
            var cashoutUrl = new CashgameCashoutUrl(bunch.Slug);
            var endGameUrl = new EndCashgameUrl(bunch.Slug);
            var cashgameIndexUrl = new CashgameIndexUrl(bunch.Slug);
            var showStartTime = cashgame.IsStarted;
            var startTime = GetStartTime(cashgame, bunch.Timezone);
            var showTable = cashgame.IsStarted;
            var showChart = cashgame.IsStarted;

            var items = GetItems(bunch, cashgame, players, isManager, now);
            var playerItems = GetPlayerItems(cashgame, players);
            var bunchPlayerItems = bunchPlayers.Select(o => new BunchPlayerItem(o.Id, o.DisplayName)).OrderBy(o => o.Name).ToList();
            var totalBuyin = new Money(cashgame.Turnover, bunch.Currency);
            var totalStacks = new Money(cashgame.TotalStacks, bunch.Currency);

            var defaultBuyin = bunch.DefaultBuyin;

            return new RunningCashgameResult(
                player.Id,
                location,
                gameDataUrl,
                playerDataUrl,
                buyinUrl,
                reportUrl,
                cashoutUrl,
                endGameUrl,
                cashgameIndexUrl,
                showStartTime,
                startTime,
                isStarted,
                showTable,
                showChart,
                items,
                playerItems,
                bunchPlayerItems,
                totalBuyin,
                totalStacks,
                defaultBuyin,
                isManager);
        }

        private static IList<int> GetPlayerIds(Cashgame cashgame)
        {
            return cashgame.Results.Select(o => o.PlayerId).ToList();
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
                var buyinUrl = new CashgameBuyinUrl(bunch.Slug);
                var reportUrl = new CashgameReportUrl(bunch.Slug);
                var cashoutUrl = new CashgameCashoutUrl(bunch.Slug);
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

        private static IList<RunningCashgamePlayerItem> GetPlayerItems(Cashgame cashgame, IList<Player> players)
        {
            var results = GetSortedResults(cashgame);
            var items = new List<RunningCashgamePlayerItem>();
            foreach (var result in results)
            {
                var playerId = result.PlayerId;
                var player = players.First(o => o.Id == playerId);
                var hasCheckedOut = result.CashoutCheckpoint != null;
                var item = new RunningCashgamePlayerItem(playerId, player.DisplayName, hasCheckedOut, result.Checkpoints);
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
