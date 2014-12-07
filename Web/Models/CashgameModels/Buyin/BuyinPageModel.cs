using Core.UseCases.BunchContext;
using Core.UseCases.BuyinForm;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Buyin
{
    public class BuyinPageModel : BunchPageModel
    {
        public bool StackFieldEnabled { get; private set; }
        public int BuyinAmount { get; private set; }
        public int StackAmount { get; private set; }

        public BuyinPageModel(BunchContextResult contextResult, BuyinFormResult buyinFormResult, BuyinPostModel postModel = null)
            : base("Buy In", contextResult)
        {
            StackFieldEnabled = buyinFormResult.CanEnterStack;
            BuyinAmount = buyinFormResult.BuyinAmount;
            if (postModel == null) return;
            BuyinAmount = postModel.AddedMoney;
            StackAmount = postModel.Stack;
        }
    }
}