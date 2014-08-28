using Application.UseCases.AppContext;
using Application.UseCases.JoinBunchConfirmation;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinBunchConfirmationPageModel : AppPageModel
    {
        public string BunchName { get; private set; }
        public string BunchUrl { get; private set; }

        public JoinBunchConfirmationPageModel(AppContextResult contextResult, JoinBunchConfirmationResult joinBunchConfirmationResult)
            : base("Welcome", contextResult)
        {
            BunchName = joinBunchConfirmationResult.BunchName;
            BunchUrl = joinBunchConfirmationResult.BunchDetailsUrl.Relative;
        }
    }
}