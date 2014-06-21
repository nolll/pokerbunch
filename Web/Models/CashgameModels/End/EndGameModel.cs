using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.End
{
    public class EndPageModel : PageModel
    {
        public EndPageModel(BunchContextResult contextResult)
            : base("End Game", contextResult)
        {
        }
    }
}