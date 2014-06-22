using System;
using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Checkpoints
{
    public class EditCheckpointPageModel : PageModel
    {
        public Url DeleteUrl { get; set; }
        public Url CancelUrl { get; set; }
        public string StackLabel { get; set; }
        public bool EnableAmountField { get; set; }
        public DateTime Timestamp { get; set; }
        public int Stack { get; set; }
        public int Amount { get; set; }

        public EditCheckpointPageModel(BunchContextResult contextResult)
            : base("Edit Checkpoint", contextResult)
        {
        }
    }
}