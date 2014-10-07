using System.Web.Mvc;
using Web.Models.ErrorModels;
using ControllerBase = Web.Controllers.Base.ControllerBase;

namespace Web.Controllers
{
    [HandleError]
    public class ErrorController : ControllerBase
    {
        [Route("-/error/notfound")]
        public ActionResult NotFound()
        {
            var contextResult = UseCase.BaseContext();

            var model = new Error404PageModel(contextResult);
            return View("Error", model);
        }

        [Route("-/error/servererror")]
        public ActionResult ServerError()
        {
            var contextResult = UseCase.BaseContext();

            var exception = Server.GetLastError();
            var message = exception != null ? exception.Message : "Unknown error";

            var model = new Error500PageModel(contextResult, message);
            return View("Error", model);
        }
    }
}
