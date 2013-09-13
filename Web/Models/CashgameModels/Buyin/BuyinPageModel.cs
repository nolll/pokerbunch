using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Buyin
{
    public class BuyinPageModel : BuyinPostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public bool StackFieldEnabled { get; set; }
	}

}