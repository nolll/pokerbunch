using JetBrains.Annotations;

namespace Web.Models.CashgameModels.Buyin
{
    public class BuyinPostModel
    {
        public string PlayerId { get; [UsedImplicitly] set; }
        public int AddedMoney { get; [UsedImplicitly] set; }
        public int Stack { get; [UsedImplicitly] set; }
    }
}