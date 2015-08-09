using Core.UseCases;
using Core.UseCases.JoinBunchConfirmation;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinBunchConfirmationPageModel : BunchPageModel
    {
        public string BunchName { get; private set; }
        public string BunchUrl { get; private set; }

        public JoinBunchConfirmationPageModel(BunchContext.Result contextResult, JoinBunchConfirmationResult joinBunchConfirmationResult)
            : base("Welcome", contextResult)
        {
            BunchName = joinBunchConfirmationResult.BunchName;
            BunchUrl = joinBunchConfirmationResult.BunchDetailsUrl.Relative;
        }
    }
}