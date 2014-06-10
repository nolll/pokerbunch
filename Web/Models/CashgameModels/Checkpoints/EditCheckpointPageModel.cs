using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Checkpoints
{
    public class EditCheckpointPageModel : EditCheckpointPostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public UrlModel DeleteUrl { get; set; }
        public UrlModel CancelUrl { get; set; }
        public string StackLabel { get; set; }
        public bool EnableAmountField { get; set; }
    }
}