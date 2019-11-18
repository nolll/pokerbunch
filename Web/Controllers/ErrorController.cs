using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(AppSettings appSettings)
            : base(appSettings)
        {
        }

        [Route(ErrorNotFoundUrl.Route)]
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return Error404();
        }

        [Route(ErrorUnauthorizedUrl.Route)]
        public ActionResult Unauthorized()
        {
            Response.StatusCode = 401;
            return Error401();
        }

        [Route(ErrorForbiddenUrl.Route)]
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return Error403();
        }

        [Route(ErrorOtherUrl.Route)]
        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            return Error500();
        }
    }
}
