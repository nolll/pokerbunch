using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.End
{
    public class EndGamePageModel : PageModel
    {
        public EndGamePageModel(BunchContextResult contextResult)
            : base("End Game", contextResult)
        {
        }
    }
}