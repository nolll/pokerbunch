using Web.Annotations;

namespace Web.Models.CashgameModels.Cashout
{
    public class CashoutPostModel
    {
        public int PlayerId { get; [UsedImplicitly] set; }
        public int Stack { get; [UsedImplicitly] set; }
    }
}