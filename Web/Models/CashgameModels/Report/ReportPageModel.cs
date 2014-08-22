using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Report
{
    public class ReportPageModel : BunchPageModel
    {
        public int? StackAmount { get; set; }

        public ReportPageModel(BunchContextResult contextResult)
            : base("Report Stack", contextResult)
        {
        }
    }
}