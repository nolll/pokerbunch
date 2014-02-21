using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Checkpoints
{
    public class EditCheckpointPageModel : EditCheckpointPostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public string DeleteUrl { get; set; }
        public string CancelUrl { get; set; }
        public string StackLabel { get; set; }
        public bool EnableAmountField { get; set; }
    }
}