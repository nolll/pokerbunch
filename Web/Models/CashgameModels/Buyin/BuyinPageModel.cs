using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Buyin
{
    public class BuyinPageModel : PageModel
    {
        public bool StackFieldEnabled { get; set; }
        public int BuyinAmount { get; set; }
        public int StackAmount { get; set; }


        public BuyinPageModel(BunchContextResult contextResult)
            : base("Buy In", contextResult)
        {
        }
    }
}