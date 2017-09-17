using System;
using System.Collections.Generic;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Checkpoints
{
    public class EditCheckpointPageModel : BunchPageModel
    {
        public string DeleteUrl { get; }
        public string CancelUrl { get; }
        public string StackLabel { get; }
        public bool EnableAmountField { get; }
        public DateTime Timestamp { get; }
        public int Stack { get; }
        public int Amount { get; }
        public ErrorListModel Errors { get; }

        public EditCheckpointPageModel(BunchContext.Result contextResult, EditCheckpointForm.Result editCheckpointFormResult, EditCheckpointPostModel postModel, IEnumerable<string> errors)
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
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Edit Checkpoint";

        public override View GetView()
        {
            return new View("~/Views/Pages/EditCheckpoint/Edit.cshtml");
        }
    }
}