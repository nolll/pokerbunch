using System.Web.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    [HandleError]
    public class ErrorController : BaseController
    {
        [Route(ErrorNotFoundUrl.Route)]
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return Error404();
        }

        [Route(ErrorUnauthorizedUrl.Route)]
        public ActionResult Unauthorized()
        {
            Response.StatusCode = 401;
            Response.TrySkipIisCustomErrors = true;
            return Error401();
        }

        [Route(ErrorForbiddenUrl.Route)]
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return Error403();
        }

        [Route(ErrorOtherUrl.Route)]
        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return Error500();
        }
    }
}
