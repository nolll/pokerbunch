using Core.UseCases.CashgameTopList;

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

        public TopListTableColumns(TopListResult topListResult)
        {
            ResultColumn = new TopListTableColumn(topListResult, ToplistSortOrder.Winnings, "Winnings");
            BuyinColumn = new TopListTableColumn(topListResult, ToplistSortOrder.Buyin, "Buyin");
            CashoutColumn = new TopListTableColumn(topListResult, ToplistSortOrder.Cashout, "Cashout");
            TimePlayedColumn = new TopListTableColumn(topListResult, ToplistSortOrder.TimePlayed, "Time");
            GamesPlayedColumn = new TopListTableColumn(topListResult, ToplistSortOrder.GamesPlayed, "Games");
            WinRateColumn = new TopListTableColumn(topListResult, ToplistSortOrder.WinRate, "Winrate");
        }
    }
}