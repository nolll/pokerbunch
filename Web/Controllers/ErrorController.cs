using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Routes;

namespace Web.Controllers
{
    [HandleError]
    public class ErrorController : BaseController
    {
        [Route(WebRoutes.Error.NotFound)]
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return Error404();
        }

        [Route(WebRoutes.Error.Unauthorized)]
        public ActionResult Unauthorized()
        {
            Response.StatusCode = 401;
            Response.TrySkipIisCustomErrors = true;
            return Error401();
        }
        
        [Route(WebRoutes.Error.Forbidden)]
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return Error403();
        }

        [Route(WebRoutes.Error.Other)]
        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return Error500();
        }
    }
}
