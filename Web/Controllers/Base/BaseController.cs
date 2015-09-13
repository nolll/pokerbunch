using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Core.Exceptions;
using Core.Services;
using Core.UseCases;
using Web.Common;
using Web.Common.Cache;
using Web.Models.ErrorModels;

namespace Web.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly Bootstrapper _bootstrapper = new Bootstrapper();

        protected UseCaseContainer UseCase
        {
            get { return _bootstrapper.UseCases; }
        }

        private BaseContext.Result GetBaseContext()
        {
            return UseCase.BaseContext.Execute();
        }
        
        protected AppContext.Result GetAppContext()
        {
            return UseCase.AppContext.Execute(new AppContext.Request(CurrentUserName));
        }

        protected BunchContext.Result GetBunchContext(string slug = null)
        {
            return UseCase.BunchContext.Execute(new BunchContext.BunchRequest(CurrentUserName, slug));
        }

        protected CashgameContext.Result GetCashgameContext(string slug, DateTime currentTime, CashgameContext.CashgamePage selectedPage = CashgameContext.CashgamePage.Unknown, int? year = null)
        {
            return UseCase.CashgameContext.Execute(new CashgameContext.Request(CurrentUserName, slug, currentTime, selectedPage, year));
        }

        protected string CurrentUserName
        {
            get
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                    return User.Identity.Name;
                return null;
            }
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

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            if (filterContext.Exception is NotFoundException)
                HandleError(filterContext, 404, Error404);
            else if(filterContext.Exception is AccessDeniedException)
                HandleError(filterContext, 401, Error401);
            else if(filterContext.Exception is NotLoggedInException)
                SignOut();
            else if(Env.IsInProduction)
                HandleError(filterContext, 500, Error500);
        }

        private void HandleError(ExceptionContext filterContext, int errorCode, Antlr.Runtime.Misc.Func<ActionResult> errorHandler)
        {
            filterContext.HttpContext.Response.StatusCode = errorCode;
            filterContext.Result = errorHandler();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.ExceptionHandled = true;
        }

        private void SignOut()
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/");
        }

        protected ActionResult Error404()
        {
            var contextResult = GetBaseContext();
            return ShowError(new Error404PageModel(contextResult));
        }

        protected ActionResult Error401()
        {
            var contextResult = GetBaseContext();
            return ShowError(new Error401PageModel(contextResult));
        }

        protected ActionResult Error403()
        {
            var contextResult = GetBaseContext();
            return ShowError(new Error403PageModel(contextResult));
        }

        protected ActionResult Error500()
        {
            var contextResult = GetBaseContext();
            return ShowError(new Error500PageModel(contextResult));
        }

        private ActionResult ShowError(ErrorPageModel model)
        {
            return View("~/Views/Error/Error.cshtml", model);
        }

        protected ActionResult JsonView(object data, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return new JsonResult(data, jsonRequestBehavior);
        }
    }
}