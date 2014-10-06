using Core.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Report
{
    public class ReportPageModel : BunchPageModel
    {
        public int? StackAmount { get; private set; }

        public ReportPageModel(BunchContextResult contextResult, ReportPostModel postModel)
            : base("Report Stack", contextResult)
        {
            if (postModel == null) return;
            StackAmount = postModel.StackAmount;
        }
    }
}