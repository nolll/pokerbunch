using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Core.Exceptions;
using Core.UseCases;
using Microsoft.ApplicationInsights;
using Web.Common;
using Web.Common.Cache;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Security;

namespace Web.Controllers.Base
{
    [EnsureHttps]
    public class BaseController : Controller
    {
        private Identity _identity;

        private Bootstrapper Bootstrapper => new Bootstrapper(SiteSettings.ConnectionString, SiteSettings.ApiUrl, SiteSettings.ApiKey, Identity.ApiToken);
        protected UseCaseContainer UseCase => Bootstrapper.UseCases;

        protected BaseContext.Result GetBaseContext()
        {
            return UseCase.BaseContext.Execute();
        }
        
        protected CoreContext.Result GetAppContext()
        {
            return UseCase.CoreContext.Execute(new CoreContext.Request(Identity.UserName));
        }

        protected BunchContext.Result GetBunchContext(string slug = null)
        {
            return UseCase.BunchContext.Execute(new BunchContext.BunchRequest(Identity.UserName, slug));
        }

        protected CashgameContext.Result GetCashgameContext(string slug, DateTime currentTime, CashgameContext.CashgamePage selectedPage = CashgameContext.CashgamePage.Unknown, int? year = null)
        {
            return UseCase.CashgameContext.Execute(new CashgameContext.Request(Identity.UserName, slug, currentTime, selectedPage, year));
        }

        protected Identity Identity
        {
            get
            {
                if (_identity == null)
                {
                    _identity = new Identity(User);
                }
                return _identity;
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
                HandleError(filterContext, 403, Error403);
            else if(filterContext.Exception is NotLoggedInException)
                SignOut();
            else if (SiteSettings.HandleErrors)
                TrackAndHandleError(filterContext, 500, Error500);
        }

        private void TrackAndHandleError(ExceptionContext filterContext, int errorCode, Antlr.Runtime.Misc.Func<ActionResult> errorHandler)
        {
            LogError(filterContext.Exception);
            HandleError(filterContext, errorCode, errorHandler);
        }

        private void LogError(Exception exception)
        {
            if (SiteSettings.EnableApplicationInsights)
            {
                var ai = new TelemetryClient();
                ai.TrackException(exception);
            }
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