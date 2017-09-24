using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinBunchConfirmationPageModel : BunchPageModel
    {
        public string BunchName { get; }
        public string BunchUrl { get; }

        public JoinBunchConfirmationPageModel(BunchContext.Result contextResult, JoinBunchConfirmation.Result joinBunchConfirmationResult)
            : base(contextResult)
        {
            BunchName = joinBunchConfirmationResult.BunchName;
            BunchUrl = new BunchDetailsUrl(joinBunchConfirmationResult.BunchId).Relative;
        }

        public override string BrowserTitle => "Welcome";

        public override View GetView()
        {
            return new View("~/Views/Pages/JoinBunch/Confirmation.cshtml");
        }
    }
}