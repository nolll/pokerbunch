using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class RunningCashgame
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;

        public RunningCashgame(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetRunning(bunch.Id);

            if(cashgame == null)
                throw new CashgameNotRunningException();

            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            var players = _playerRepository.GetList(GetPlayerIds(cashgame));
            var bunchPlayers = _playerRepository.GetList(bunch.Id);

            var isStarted = cashgame.IsStarted;
            var isManager = RoleHandler.IsInRole(user, player, Role.Manager);
            
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

            var items = GetItems(bunch, cashgame, players, request.CurrentTime);
            var playerItems = GetPlayerItems(bunch.Slug, cashgame, players);
            var bunchPlayerItems = bunchPlayers.Select(o => new BunchPlayerItem(o.Id, o.DisplayName)).OrderBy(o => o.Name).ToList();
            var totalBuyin = new Money(cashgame.Turnover, bunch.Currency);
            var totalStacks = new Money(cashgame.TotalStacks, bunch.Currency);

            var defaultBuyin = bunch.DefaultBuyin;

            return new Result(
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

        private static IList<RunningCashgameTableItem> GetItems(Bunch bunch, Cashgame cashgame, IList<Player> players, DateTime now)
        {
            var results = GetSortedResults(cashgame);
            var items = new List<RunningCashgameTableItem>();
            foreach (var result in results)
            {
                var playerId = result.PlayerId;
                var player = players.First(o => o.Id == playerId);

                var name = player.DisplayName;
                var playerUrl = new CashgameActionUrl(bunch.Slug, cashgame.DateString, player.Id);
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
                    hasCashedOut);

                items.Add(item);
            }

            return items;
        }

        private static IList<RunningCashgamePlayerItem> GetPlayerItems(string slug, Cashgame cashgame, IList<Player> players)
        {
            var results = GetSortedResults(cashgame);
            var items = new List<RunningCashgamePlayerItem>();
            foreach (var result in results)
            {
                var playerId = result.PlayerId;
                var player = players.First(o => o.Id == playerId);
                var playerUrl = new CashgameActionUrl(slug, cashgame.DateString, playerId);
                var hasCheckedOut = result.CashoutCheckpoint != null;
                var item = new RunningCashgamePlayerItem(playerId, player.DisplayName, playerUrl, hasCheckedOut, result.Checkpoints);
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

        public class Request
        {
            public string Slug { get; private set; }
            public string UserName { get; private set; }
            public DateTime CurrentTime { get; private set; }

            public Request(string slug, string userName, DateTime currentTime)
            {
                Slug = slug;
                UserName = userName;
                CurrentTime = currentTime;
            }
        }

        public class Result
        {
            public int PlayerId { get; private set; }
            public string Location { get; private set; }
            public Url GameDataUrl { get; private set; }
            public Url PlayersDataUrl { get; private set; }
            public Url BuyinUrl { get; private set; }
            public Url ReportUrl { get; private set; }
            public Url CashoutUrl { get; private set; }
            public Url EndGameUrl { get; private set; }
            public Url CashgameIndexUrl { get; private set; }
            public bool ShowStartTime { get; private set; }
            public string StartTime { get; private set; }
            public bool IsStarted { get; private set; }
            public bool ShowTable { get; private set; }
            public bool ShowChart { get; private set; }
            public IList<RunningCashgameTableItem> Items { get; private set; }
            public IList<RunningCashgamePlayerItem> PlayerItems { get; private set; }
            public IList<BunchPlayerItem> BunchPlayerItems { get; private set; }
            public Money TotalBuyin { get; private set; }
            public Money TotalStacks { get; private set; }
            public int DefaultBuyin { get; private set; }
            public bool IsManager { get; private set; }

            public Result(
                int playerId,
                string location,
                Url gameDataUrl,
                Url playersDataUrl,
                Url buyinUrl,
                Url reportUrl,
                Url cashoutUrl,
                Url endGameUrl,
                Url cashgameIndexUrl,
                bool showStartTime,
                string startTime,
                bool isStarted,
                bool showTable,
                bool showChart,
                IList<RunningCashgameTableItem> items,
                IList<RunningCashgamePlayerItem> playerItems,
                IList<BunchPlayerItem> bunchPlayerItems,
                Money totalBuyin,
                Money totalStacks,
                int defaultBuyin,
                bool isManager)
            {
                PlayerId = playerId;
                Location = location;
                PlayersDataUrl = playersDataUrl;
                GameDataUrl = gameDataUrl;
                BuyinUrl = buyinUrl;
                ReportUrl = reportUrl;
                CashoutUrl = cashoutUrl;
                EndGameUrl = endGameUrl;
                CashgameIndexUrl = cashgameIndexUrl;
                ShowStartTime = showStartTime;
                StartTime = startTime;
                IsStarted = isStarted;
                ShowTable = showTable;
                ShowChart = showChart;
                Items = items;
                PlayerItems = playerItems;
                BunchPlayerItems = bunchPlayerItems;
                TotalBuyin = totalBuyin;
                TotalStacks = totalStacks;
                DefaultBuyin = defaultBuyin;
                IsManager = isManager;
            }
        }

        public class BunchPlayerItem
        {
            public int PlayerId { get; private set; }
            public string Name { get; private set; }

            public BunchPlayerItem(int playerId, string name)
            {
                PlayerId = playerId;
                Name = name;
            }
        }

        public class RunningCashgameCheckpointItem
        {
            public DateTime Time { get; private set; }
            public int Stack { get; private set; }
            public int AddedMoney { get; private set; }

            public RunningCashgameCheckpointItem(Checkpoint checkpoint)
            {
                Time = checkpoint.Timestamp;
                Stack = checkpoint.Stack;
                AddedMoney = checkpoint.Amount;
            }
        }

        public class RunningCashgamePlayerItem
        {
            public int PlayerId { get; private set; }
            public string Name { get; private set; }
            public Url PlayerUrl { get; private set; }
            public bool HasCashedOut { get; private set; }
            public IList<RunningCashgameCheckpointItem> Checkpoints { get; private set; }

            public RunningCashgamePlayerItem(int playerId, string name, Url playerUrl, bool hasCashedOut, IEnumerable<Checkpoint> checkpoints)
            {
                PlayerId = playerId;
                Name = name;
                PlayerUrl = playerUrl;
                HasCashedOut = hasCashedOut;
                Checkpoints = checkpoints.Select(o => new RunningCashgameCheckpointItem(o)).ToList();
            }
        }

        public class RunningCashgameTableItem
        {
            public string Name { get; private set; }
            public Url PlayerUrl { get; private set; }
            public Money Buyin { get; private set; }
            public Money Stack { get; private set; }
            public Money Winnings { get; private set; }
            public Time Time { get; private set; }
            public bool HasCashedOut { get; private set; }

            public RunningCashgameTableItem(string name, Url playerUrl, Money buyin, Money stack, Money winnings, Time time, bool hasCashedOut)
            {
                Name = name;
                PlayerUrl = playerUrl;
                Buyin = buyin;
                Stack = stack;
                Winnings = winnings;
                Time = time;
                HasCashedOut = hasCashedOut;
            }
        }
    }
}
