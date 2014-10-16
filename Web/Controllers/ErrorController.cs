using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.ErrorModels;

namespace Web.Controllers
{
    [HandleError]
    public class ErrorController : PokerBunchController
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

            var model = new Error500PageModel(contextResult);
            return View("Error", model);
        }
    }
}
