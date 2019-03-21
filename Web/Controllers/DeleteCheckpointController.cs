using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCheckpointController : BaseController
    {
        [Authorize]
        [Route(DeleteActionUrl.Route)]
        public ActionResult DeleteCheckpoint(string cashgameId, string actionId)
        {
            var request = new DeleteCheckpoint.Request(cashgameId, actionId);
            var result = UseCase.DeleteCheckpoint.Execute(request);
            var returnUrl = new CashgameDetailsUrl(result.Slug, result.CashgameId);
            return Redirect(returnUrl.Relative);
        }
    }
}