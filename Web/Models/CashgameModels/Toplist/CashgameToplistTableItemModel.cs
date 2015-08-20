using Core.Services;
using Core.UseCases;
using Web.Urls;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistTableItemModel
    {
        public int Rank { get; private set; }
        public string Name { get; private set; }
        public string TotalResult { get; private set; }
        public string ResultSortClass { get; private set; }
        public string Buyin { get; private set; }
        public string BuyinSortClass { get; private set; }
        public string Cashout { get; private set; }
        public string CashoutSortClass { get; private set; }
        public string ResultClass { get; private set; }
        public string GameTime { get; private set; }
        public string GameTimeSortClass { get; private set; }
        public int GameCount { get; private set; }
        public string GameCountSortClass { get; private set; }
        public string WinRate { get; private set; }
        public string WinRateSortClass { get; private set; }
        public string PlayerUrl { get; private set; }

        public CashgameToplistTableItemModel(TopList.Item toplistItem, TopList.SortOrder sortOrder)
        {
            Rank = toplistItem.Rank;
            TotalResult = toplistItem.Winnings.String;
            Buyin = toplistItem.Buyin.String;
            Cashout = toplistItem.Cashout.String;
            ResultClass = ResultFormatter.GetWinningsCssClass(toplistItem.Winnings);
            GameTime = toplistItem.TimePlayed.String;
            GameCount = toplistItem.GamesPlayed;
            WinRate = toplistItem.WinRate.String;
            Name = toplistItem.Name;
            PlayerUrl = new PlayerDetailsUrl(toplistItem.PlayerId).Relative;
            ResultSortClass = GetSortCssClass(sortOrder, TopList.SortOrder.Winnings);
            BuyinSortClass = GetSortCssClass(sortOrder, TopList.SortOrder.Buyin);
            CashoutSortClass = GetSortCssClass(sortOrder, TopList.SortOrder.Cashout);
            GameTimeSortClass = GetSortCssClass(sortOrder, TopList.SortOrder.TimePlayed);
            GameCountSortClass = GetSortCssClass(sortOrder, TopList.SortOrder.GamesPlayed);
            WinRateSortClass = GetSortCssClass(sortOrder, TopList.SortOrder.WinRate);
        }

        private string GetSortCssClass(TopList.SortOrder selectedSortOrder, TopList.SortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "table-list--sortable__sort-item" : "";
        }
    }
}