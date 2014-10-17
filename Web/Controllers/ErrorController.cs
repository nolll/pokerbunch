using System.Web.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers
{
    [HandleError]
    public class ErrorController : PokerBunchController
    {
        [Route("-/error/notfound")]
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return Error404();
        }

        [Route("-/error/servererror")]
        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return Error500();
        }
    }
}
