using System.Web.Mvc;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Urls;

namespace Web.Controllers
{
    [HandleError]
    public class ErrorController : BaseController
    {
        [Route(WebRoutes.ErrorNotFound)]
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return Error404();
        }

        [Route(WebRoutes.ErrorUnauthorized)]
        public ActionResult Unauthorized()
        {
            Response.StatusCode = 401;
            Response.TrySkipIisCustomErrors = true;
            return Error401();
        }
        
        [Route(WebRoutes.ErrorForbidden)]
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return Error403();
        }

        [Route(WebRoutes.ErrorOther)]
        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return Error500();
        }
    }
}
