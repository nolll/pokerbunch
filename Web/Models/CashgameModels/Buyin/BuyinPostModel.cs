using Web.Annotations;

namespace Web.Models.CashgameModels.Buyin
{
    public class BuyinPostModel
    {
        public int BuyinAmount { get; [UsedImplicitly] set; }
        public int StackAmount { get; [UsedImplicitly] set; }
    }
}