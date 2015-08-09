using System;
using Core.UseCases;
using Core.UseCases.EditCheckpointForm;
using Web.Models.PageBaseModels;

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

        public EditCheckpointPageModel(BunchContext.Result contextResult, EditCheckpointFormResult editCheckpointFormResult, EditCheckpointPostModel postModel)
            : base("Edit Checkpoint", contextResult)
        {
            Stack = editCheckpointFormResult.Stack;
            Amount = editCheckpointFormResult.Amount;
            Timestamp = editCheckpointFormResult.TimeStamp;
            DeleteUrl = editCheckpointFormResult.DeleteUrl.Relative;
            CancelUrl = editCheckpointFormResult.CancelUrl.Relative;
            EnableAmountField = editCheckpointFormResult.CanEditAmount;
            StackLabel = editCheckpointFormResult.CanEditAmount ? "Stack after buyin" : "Stack";
            if (postModel == null) return;
            Stack = postModel.Stack;
            Amount = postModel.Amount;
            Timestamp = postModel.Timestamp;
        }
    }
}