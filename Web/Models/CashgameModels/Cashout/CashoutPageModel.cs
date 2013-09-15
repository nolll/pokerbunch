using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Cashout
{
    public class CashoutPageModel : CashoutPostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
	}

}