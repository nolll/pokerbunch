using Core.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.End
{
    public class EndGamePageModel : BunchPageModel
    {
        public EndGamePageModel(BunchContextResult contextResult)
            : base("End Game", contextResult)
        {
        }
    }
}