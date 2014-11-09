using System.Collections.Generic;
using Core.Entities;
using Core.Urls;

namespace Core.UseCases.RunningCashgame
{
    public class RunningCashgameResult
    {
        public string Location { get; private set; }
        public Url BuyinUrl { get; private set; }
        public Url ReportUrl { get; private set; }
        public Url CashoutUrl { get; private set; }
        public Url EndGameUrl { get; private set; }
        public bool ShowStartTime { get; private set; }
        public string StartTime { get; private set; }
        public bool IsStarted { get; private set; }
        public bool BuyinButtonEnabled { get; private set; }
        public bool ReportButtonEnabled { get; private set; }
        public bool CashoutButtonEnabled { get; private set; }
        public bool EndGameButtonEnabled { get; private set; }
        public bool ShowTable { get; private set; }
        public bool ShowChart { get; private set; }
        public IList<RunningCashgameTableItem> Items { get; private set; }
        public Money TotalBuyin { get; private set; }
        public Money TotalStacks { get; private set; }

        public RunningCashgameResult(
            string location,
            Url buyinUrl,
            Url reportUrl,
            Url cashoutUrl,
            Url endGameUrl,
            bool showStartTime,
            string startTime,
            bool isStarted,
            bool buyinButtonEnabled,
            bool reportButtonEnabled,
            bool cashoutButtonEnabled,
            bool endGameButtonEnabled,
            bool showTable,
            bool showChart,
            IList<RunningCashgameTableItem> items,
            Money totalBuyin,
            Money totalStacks)
        {
            Location = location;
            BuyinUrl = buyinUrl;
            ReportUrl = reportUrl;
            CashoutUrl = cashoutUrl;
            EndGameUrl = endGameUrl;
            ShowStartTime = showStartTime;
            StartTime = startTime;
            IsStarted = isStarted;
            BuyinButtonEnabled = buyinButtonEnabled;
            ReportButtonEnabled = reportButtonEnabled;
            CashoutButtonEnabled = cashoutButtonEnabled;
            EndGameButtonEnabled = endGameButtonEnabled;
            ShowTable = showTable;
            ShowChart = showChart;
            Items = items;
            TotalBuyin = totalBuyin;
            TotalStacks = totalStacks;
        }
    }
}