using System;
using System.Web;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Microsoft.ApplicationInsights;
using Plumbing;
using Web.Extensions;
using Web.Models;
using Web.Models.ErrorModels;
using Web.Security;

namespace Web.Controllers.Base
{
    [EnsureHttps]
    public class BaseController : Controller
    {
        private IUserIdentity _identity;

        private Bootstrapper Bootstrapper => new Bootstrapper(SiteSettings.ApiHost, SiteSettings.ApiProtocol, SiteSettings.ApiKey, Identity.ApiToken, SiteSettings.DetailedErrorsForApi, SiteSettings.UseFakeData);
        protected UseCaseContainer UseCase => Bootstrapper.UseCases;

        protected CoreContext.Result GetAppContext()
        {
            return UseCase.CoreContext.Execute(new CoreContext.Request(Identity.UserName));
        }

        protected BunchContext.Result GetBunchContext(string bunchId)
        {
            return UseCase.BunchContext.Execute(GetAppContext(), new BunchContext.Request(bunchId));
        }

        protected IUserIdentity Identity => _identity ?? (_identity = SiteSettings.UseFakeData ? (IUserIdentity)new FakeUserIdentity() : new UserIdentity(User));

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            if (filterContext.Exception is NotFoundException)
                HandleError(filterContext, 404, Error404);
            else if(filterContext.Exception is AccessDeniedException)
                HandleError(filterContext, 403, Error403);
            else if(filterContext.Exception is NotLoggedInException)
                HandleAuthCookieError(filterContext, 200, ErrorAuthCookie);
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

        private void HandleError(ExceptionContext filterContext, int responseCode, Antlr.Runtime.Misc.Func<ActionResult> errorHandler)
        {
            filterContext.HttpContext.Response.StatusCode = responseCode;
            filterContext.Result = errorHandler();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.ExceptionHandled = true;
        }

        private void HandleAuthCookieError(ExceptionContext filterContext, int responseCode, Antlr.Runtime.Misc.Func<ActionResult> errorHandler)
        {
            ClearAllCookies(filterContext);
            HandleError(filterContext, responseCode, errorHandler);
        }

        private void ClearAllCookies(ExceptionContext filterContext)
        {
            for (var i = 0; i < filterContext.HttpContext.Request.Cookies.Count; i++)
            {
                var cookie = filterContext.HttpContext.Request.Cookies[i];
                if (cookie == null) continue;
                var cookieName = cookie.Name;
                var clearedCookie = new HttpCookie(cookieName) {Expires = DateTime.Now.AddDays(-1)};
                filterContext.HttpContext.Response.Cookies.Add(clearedCookie);
            }
        }

        protected ActionResult Error404()
        {
            return ShowError(new Error404PageModel());
        }

        protected ActionResult Error401()
        {
            return ShowError(new Error401PageModel());
        }

        protected ActionResult Error403()
        {
            return ShowError(new Error403PageModel());
        }

        protected ActionResult Error500()
        {
            return ShowError(new Error500PageModel());
        }

        protected ActionResult ErrorAuthCookie()
        {
            return ShowError(new ErrorAuthCookiePageModel());
        }

        private ActionResult ShowError(ErrorPageModel model)
        {
            return View(model);
        }

        protected ActionResult JsonView(object data, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return new JsonResult(data, jsonRequestBehavior);
        }

        protected ViewResult View(IViewModel model)
        {
            return View(model.GetView(), model);
        }
    }
}