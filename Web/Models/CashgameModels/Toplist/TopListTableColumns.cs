using Core.UseCases;

namespace Web.Models.CashgameModels.Toplist
{
    public class TopListTableColumns
    {
        public TopListTableColumn ResultColumn { get; private set; }
        public TopListTableColumn BuyinColumn { get; private set; }
        public TopListTableColumn CashoutColumn { get; private set; }
        public TopListTableColumn TimePlayedColumn { get; private set; }
        public TopListTableColumn GamesPlayedColumn { get; private set; }
        public TopListTableColumn WinRateColumn { get; private set; }

        public TopListTableColumns(TopList.Result topListResult)
        {
            ResultColumn = new TopListTableColumn(topListResult, TopList.SortOrder.Winnings, "Winnings");
            BuyinColumn = new TopListTableColumn(topListResult, TopList.SortOrder.Buyin, "Buyin");
            CashoutColumn = new TopListTableColumn(topListResult, TopList.SortOrder.Cashout, "Cashout");
            TimePlayedColumn = new TopListTableColumn(topListResult, TopList.SortOrder.TimePlayed, "Time");
            GamesPlayedColumn = new TopListTableColumn(topListResult, TopList.SortOrder.GamesPlayed, "Games");
            WinRateColumn = new TopListTableColumn(topListResult, TopList.SortOrder.WinRate, "Winrate");
        }
    }

    public class CurrentRankingsTableColumns
    {
        public TopListTableColumn ResultColumn { get; private set; }
        public TopListTableColumn BuyinColumn { get; private set; }
        public TopListTableColumn CashoutColumn { get; private set; }
        public TopListTableColumn TimePlayedColumn { get; private set; }
        public TopListTableColumn GamesPlayedColumn { get; private set; }
        public TopListTableColumn WinRateColumn { get; private set; }

        public CurrentRankingsTableColumns(TopList.Result topListResult)
        {
            ResultColumn = new TopListTableColumn(topListResult, TopList.SortOrder.Winnings, "Winnings");
            BuyinColumn = new TopListTableColumn(topListResult, TopList.SortOrder.Buyin, "Buyin");
            CashoutColumn = new TopListTableColumn(topListResult, TopList.SortOrder.Cashout, "Cashout");
            TimePlayedColumn = new TopListTableColumn(topListResult, TopList.SortOrder.TimePlayed, "Time");
            GamesPlayedColumn = new TopListTableColumn(topListResult, TopList.SortOrder.GamesPlayed, "Games");
            WinRateColumn = new TopListTableColumn(topListResult, TopList.SortOrder.WinRate, "Winrate");
        }
    }
}