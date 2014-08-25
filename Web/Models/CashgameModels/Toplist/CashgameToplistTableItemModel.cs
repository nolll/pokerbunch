using System.Web;
using Application.Services;
using Application.Urls;
using Application.UseCases.CashgameTopList;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistTableItemModel
    {
        public int Rank { get; private set; }
        public string Name { get; private set; }
        public string UrlEncodedName { get; private set; }
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
        public Url PlayerUrl { get; private set; }

        public CashgameToplistTableItemModel(TopListItem toplistItem, string slug, ToplistSortOrder sortOrder)
        {
            Rank = toplistItem.Rank;
            TotalResult = toplistItem.Winnings.ToString();
            Buyin = toplistItem.Buyin.ToString();
            Cashout = toplistItem.Cashout.ToString();
            ResultClass = ResultFormatter.GetWinningsCssClass(toplistItem.Winnings);
            GameTime = toplistItem.TimePlayed.ToString();
            GameCount = toplistItem.GamesPlayed;
            WinRate = toplistItem.WinRate.ToString();
            Name = toplistItem.Name;
            UrlEncodedName = HttpUtility.UrlPathEncode(toplistItem.Name);
            PlayerUrl = new PlayerDetailsUrl(slug, toplistItem.PlayerId);
            ResultSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Winnings);
            BuyinSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Buyin);
            CashoutSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Cashout);
            GameTimeSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.TimePlayed);
            GameCountSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.GamesPlayed);
            WinRateSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.WinRate);
        }

        private string GetSortCssClass(ToplistSortOrder selectedSortOrder, ToplistSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}