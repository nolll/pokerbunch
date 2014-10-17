using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Core.Exceptions;
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
            {
                filterContext.HttpContext.Response.StatusCode = 404;
                filterContext.Result = Error404();
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = Error500();
            }

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