using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.HomegameModels.Join
{
    public class JoinBunchConfirmationPageModel : BunchPageModel
    {
        public string BunchName { get; private set; }
        public string BunchUrl { get; private set; }

        public JoinBunchConfirmationPageModel(BunchContext.Result contextResult, JoinBunchConfirmation.Result joinBunchConfirmationResult)
            : base(contextResult)
        {
            BunchName = joinBunchConfirmationResult.BunchName;
            BunchUrl = new BunchDetailsUrl(joinBunchConfirmationResult.BunchId).Relative;
        }

        public override string BrowserTitle => "Welcome";
    }
}