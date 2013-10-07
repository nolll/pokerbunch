using System.ComponentModel.DataAnnotations;

namespace Web.Models.CashgameModels.Report
{
    public class ReportPostModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Stack needs to be positive")]
        public int StackAmount { get; set; }
    }
}