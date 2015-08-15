using System.Web.Mvc;
using Core.Urls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    [HandleError]
    public class ErrorController : BaseController
    {
        [Route(Routes.ErrorNotFound)]
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return Error404();
        }

        [Route(Routes.ErrorUnauthorized)]
        public ActionResult Unauthorized()
        {
            Response.StatusCode = 401;
            Response.TrySkipIisCustomErrors = true;
            return Error401();
        }
        
        [Route(Routes.ErrorForbidden)]
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return Error403();
        }

        [Route(Routes.ErrorOther)]
        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return Error500();
        }
    }
}
