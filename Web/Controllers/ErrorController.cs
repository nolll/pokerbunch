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
            return ShowError(new Error404PageModel(contextResult));
        }

        [Route("-/error/servererror")]
        public ActionResult ServerError()
        {
            var contextResult = UseCase.BaseContext();
            return ShowError(new Error500PageModel(contextResult));
        }

        private ActionResult ShowError(ErrorPageModel model)
        {
            return View("~/Views/Error/Error.cshtml", model);
        }
    }
}
