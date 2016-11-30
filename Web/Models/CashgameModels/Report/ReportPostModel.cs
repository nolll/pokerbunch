using JetBrains.Annotations;

namespace Web.Models.CashgameModels.Report
{
    public class ReportPostModel
    {
        public string PlayerId { get; [UsedImplicitly] set; }
        public int Stack { get; [UsedImplicitly] set; }
    }
}