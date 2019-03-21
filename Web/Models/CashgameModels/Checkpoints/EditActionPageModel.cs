using System;
using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Checkpoints
{
    public class EditActionPageModel : BunchPageModel
    {
        public string DeleteUrl { get; }
        public string CancelUrl { get; }
        public string StackLabel { get; }
        public bool EnableAmountField { get; }
        public DateTime Timestamp { get; }
        public int Stack { get; }
        public int Amount { get; }
        public ErrorListModel Errors { get; }

        public EditActionPageModel(BunchContext.Result contextResult, EditActionForm.Result editActionFormResult, EditActionPostModel postModel, IEnumerable<string> errors)
            : base(contextResult)
        {
            Stack = editActionFormResult.Stack;
            Amount = editActionFormResult.Amount;
            Timestamp = editActionFormResult.TimeStamp;
            DeleteUrl = new DeleteActionUrl(editActionFormResult.CashgameId, editActionFormResult.ActionId).Relative;
            CancelUrl = new CashgameActionUrl(editActionFormResult.CashgameId, editActionFormResult.PlayerId).Relative;
            EnableAmountField = editActionFormResult.CanEditAmount;
            StackLabel = editActionFormResult.CanEditAmount ? "Stack after buyin" : "Stack";
            if (postModel == null) return;
            Stack = postModel.Stack;
            Amount = postModel.Amount;
            Timestamp = postModel.Timestamp;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Edit Action";

        public override View GetView()
        {
            return new View("~/Views/Pages/EditAction/Edit.cshtml");
        }
    }
}