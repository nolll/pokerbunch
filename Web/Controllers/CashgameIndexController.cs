using System.Web.Mvc;
using Core.Urls;
using Core.UseCases.CashgameContext;
using Web.Security.Attributes;
using ControllerBase = Web.Controllers.Base.ControllerBase;

namespace Web.Controllers
{
    public class CashgameIndexController : ControllerBase
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/index")]
        public ActionResult Index(string slug)
        {
            var result = UseCase.CashgameContext(new CashgameContextRequest(slug));
            var url = GetIndexUrl(result);
            return Redirect(url.Relative);
        }

        private Url GetIndexUrl(CashgameContextResult result)
        {
            if (result.LatestYear.HasValue)
                return new CashgameMatrixUrl(result.BunchContext.Slug, result.LatestYear);
            return new AddCashgameUrl(result.BunchContext.Slug);
        }
    }
}