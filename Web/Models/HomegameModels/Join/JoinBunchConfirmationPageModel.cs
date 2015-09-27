using Core.UseCases;
using Web.Common.Urls.SiteUrls;
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
            BunchUrl = new BunchDetailsUrl(joinBunchConfirmationResult.Slug).Relative;
        }
    }
}