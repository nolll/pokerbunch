using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Core.Exceptions;
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
                filterContext.Result = new ErrorController().NotFound();
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new ErrorController().ServerError();
            }

            //var viewName = GetViewName(filterContext.Exception);
            //filterContext.Result = new ViewResult(
            //{
            //    ViewName = "~/Views/Error/404.cshtml",
            //};

            filterContext.ExceptionHandled = true;
        }

        private string GetViewName(Exception ex)
        {
            if (ex is NotFoundException)
                return "~/Views/Error/404.cshtml";
            return "~/Views/Error/500.cshtml";
        }
    }
}