using System.ComponentModel.DataAnnotations;
using Web.Annotations;

namespace Web.Models.CashgameModels.Cashout
{
    public class CashoutPostModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
        public int? StackAmount { get; [UsedImplicitly] set; }
    }
}