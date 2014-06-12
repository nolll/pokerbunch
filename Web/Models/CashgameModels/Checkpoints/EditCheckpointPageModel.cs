using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Checkpoints
{
    public class EditCheckpointPageModel : EditCheckpointPostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public Url DeleteUrl { get; set; }
        public Url CancelUrl { get; set; }
        public string StackLabel { get; set; }
        public bool EnableAmountField { get; set; }
    }
}