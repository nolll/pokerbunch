using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinBunchConfirmationPageModel : BunchPageModel
    {
        public string BunchName { get; private set; }
        public string BunchUrl { get; private set; }

        public JoinBunchConfirmationPageModel(BunchContext.Result contextResult, JoinBunchConfirmation.Result joinBunchConfirmationResult)
            : base("Welcome", contextResult)
        {
            BunchName = joinBunchConfirmationResult.BunchName;
            BunchUrl = joinBunchConfirmationResult.BunchDetailsUrl.Relative;
        }
    }
}