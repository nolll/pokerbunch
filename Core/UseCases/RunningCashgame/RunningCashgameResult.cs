using System.Collections.Generic;
using Core.Entities;
using Core.Urls;

namespace Core.UseCases.RunningCashgame
{
    public class RunningCashgameResult
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

        public RunningCashgameResult(
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
}