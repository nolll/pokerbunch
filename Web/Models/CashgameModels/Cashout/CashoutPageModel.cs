using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Cashout
{
    public class CashoutPageModel : PageModel
    {
        public int? StackAmount { get; private set; }

        public CashoutPageModel(BunchContextResult contextResult, CashoutPostModel postModel)
            : base("Cash Out", contextResult)
        {
            if (postModel == null) return;
            StackAmount = postModel.StackAmount;
        }
    }
}