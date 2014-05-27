using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.End
{
    public class EndPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public bool ShowDiff { get; set; }
    }
}