using Web.Annotations;

namespace Web.Models.CashgameModels.Cashout
{
    public class CashoutPostModel
    {
        public int StackAmount { get; [UsedImplicitly] set; }
    }
}