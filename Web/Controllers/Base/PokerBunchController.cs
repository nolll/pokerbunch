using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using Core.Exceptions;
using Core.Services;
using Web.Models.ErrorModels;
using Web.Plumbing;

namespace Web.Controllers.Base
{
    public class PokerBunchController : Controller
    {
        protected UseCaseContainer UseCase
        {
            get { return UseCaseContainer.Instance; }
        }

        protected void AddModelErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                AddModelError(error);
            }
        }

        protected void AddModelError(string error)
        {
            ModelState.AddModelError(error, error);
        }
        
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult(data, contentType, contentEncoding, behavior);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            if (filterContext.Exception is NotFoundException)
                HandleError(filterContext, 404, Error404);
            else if(filterContext.Exception is AccessDeniedException)
                HandleError(filterContext, 401, Error401);
            else if(Env.IsInProduction)
                HandleError(filterContext, 500, Error500);
        }

        private void HandleError(ExceptionContext filterContext, int errorCode, Func<ActionResult> errorHandler)
        {
            filterContext.HttpContext.Response.StatusCode = errorCode;
            filterContext.Result = errorHandler();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.ExceptionHandled = true;
        }

        protected ActionResult Error404()
        {
            var contextResult = UseCase.BaseContext();
            return ShowError(new Error404PageModel(contextResult));
        }

        protected ActionResult Error401()
        {
            var contextResult = UseCase.BaseContext();
            return ShowError(new Error401PageModel(contextResult));
        }

        protected ActionResult Error403()
        {
            var contextResult = UseCase.BaseContext();
            return ShowError(new Error403PageModel(contextResult));
        }

        protected ActionResult Error500()
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