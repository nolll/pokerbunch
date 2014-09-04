using System.Web.Mvc;
using Application.UseCases.BaseContext;
using Web.Models.ErrorModels;

namespace Web.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        private readonly IBaseContextInteractor _contextInteractor;

        public ErrorController(IBaseContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        [Route("-/error/notfound")]
        public ActionResult NotFound()
        {
            var contextResult = _contextInteractor.Execute();

            var model = new Error404PageModel(contextResult);
            return View("Error", model);
        }

        [Route("-/error/servererror")]
        public ActionResult ServerError()
        {
            var contextResult = _contextInteractor.Execute();

            var exception = Server.GetLastError();
            var message = exception != null ? exception.Message : "Unknown error";

            var model = new Error500PageModel(contextResult, message);
            return View("Error", model);
        }
    }
}
