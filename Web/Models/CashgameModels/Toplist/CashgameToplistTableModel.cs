using System.Collections.Generic;

namespace Web.Models.CashgameModels.Toplist
{
	public class CashgameToplistTableModel
    {
	    public IList<CashgameToplistTableItemModel> ItemModels { get; set; }
        public string ResultSortClass { get; set; }
        public string ResultSortUrl { get; set; }
        public string BuyinSortClass { get; set; }
        public string BuyinSortUrl { get; set; }
        public string CashoutSortClass { get; set; }
        public string CashoutSortUrl { get; set; }
        public string GameTimeSortClass { get; set; }
        public string GameTimeSortUrl { get; set; }
        public string GameCountSortClass { get; set; }
        public string GameCountSortUrl { get; set; }
        public string WinRateSortClass { get; set; }
        public string WinRateSortUrl { get; set; }
    }
}