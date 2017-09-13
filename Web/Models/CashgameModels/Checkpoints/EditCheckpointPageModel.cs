using System;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Checkpoints
{
    public class EditCheckpointPageModel : BunchPageModel
    {
        public string DeleteUrl { get; private set; }
        public string CancelUrl { get; private set; }
        public string StackLabel { get; private set; }
        public bool EnableAmountField { get; private set; }
        public DateTime Timestamp { get; private set; }
        public int Stack { get; private set; }
        public int Amount { get; private set; }

        public EditCheckpointPageModel(BunchContext.Result contextResult, EditCheckpointForm.Result editCheckpointFormResult, EditCheckpointPostModel postModel)
            : base(contextResult)
        {
            Stack = editCheckpointFormResult.Stack;
            Amount = editCheckpointFormResult.Amount;
            Timestamp = editCheckpointFormResult.TimeStamp;
            DeleteUrl = new DeleteCheckpointUrl(editCheckpointFormResult.CashgameId, editCheckpointFormResult.ActionId).Relative;
            CancelUrl = new CashgameActionUrl(editCheckpointFormResult.CashgameId, editCheckpointFormResult.PlayerId).Relative;
            EnableAmountField = editCheckpointFormResult.CanEditAmount;
            StackLabel = editCheckpointFormResult.CanEditAmount ? "Stack after buyin" : "Stack";
            if (postModel == null) return;
            Stack = postModel.Stack;
            Amount = postModel.Amount;
            Timestamp = postModel.Timestamp;
        }

        public override string BrowserTitle => "Edit Checkpoint";

        public override View GetView()
        {
            return new View("~/Views/Pages/EditCheckpoint/Edit.cshtml");
        }
    }
}