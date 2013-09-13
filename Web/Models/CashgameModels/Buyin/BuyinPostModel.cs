using System.ComponentModel.DataAnnotations;

namespace Web.Models.CashgameModels.Buyin
{
    public class BuyinPostModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Amount needs to be positive")]
        public int BuyinAmount { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Stack can't be negative")]
        public int StackAmount { get; set; }
    }
}