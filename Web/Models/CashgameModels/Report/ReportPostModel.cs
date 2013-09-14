using System.ComponentModel.DataAnnotations;

namespace Web.Models.CashgameModels.Buyin
{
    public class ReportPostModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Stack can't be negative")]
        public int StackAmount { get; set; }
    }
}