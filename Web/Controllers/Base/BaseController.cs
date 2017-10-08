using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core.Exceptions;
using Core.UseCases;
using Microsoft.ApplicationInsights;
using Plumbing;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models;
using Web.Models.ErrorModels;
using Web.Security;

namespace Web.Controllers.Base
{
    [EnsureHttps]
    public class BaseController : Controller
    {
        private Identity _identity;

        private Bootstrapper Bootstrapper => new Bootstrapper(SiteSettings.ApiHost, SiteSettings.ApiProtocol, SiteSettings.ApiKey, Identity.ApiToken, SiteSettings.DetailedErrorsForApi);
        protected UseCaseContainer UseCase => Bootstrapper.UseCases;

        protected BaseContext.Result GetBaseContext()
        {
            return UseCase.BaseContext.Execute();
        }
        
        protected CoreContext.Result GetAppContext()
        {
            return UseCase.CoreContext.Execute(GetBaseContext(), new CoreContext.Request(Identity.UserName));
        }

        protected BunchContext.Result GetBunchContext(string bunchId = null)
        {
            return UseCase.BunchContext.Execute(GetAppContext(), new BunchContext.Request(bunchId));
        }

        protected CashgameContext.Result GetCashgameContext(string bunchId, DateTime currentTime, CashgameContext.CashgamePage selectedPage = CashgameContext.CashgamePage.Unknown, int? year = null)
        {
            return UseCase.CashgameContext.Execute(GetBunchContext(bunchId), new CashgameContext.Request(bunchId, currentTime, selectedPage, year));
        }

        protected Identity Identity => _identity ?? (_identity = new Identity(User));

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            if (filterContext.Exception is NotFoundException)
                HandleError(filterContext, 404, Error404);
            else if(filterContext.Exception is AccessDeniedException)
                HandleError(filterContext, 403, Error403);
            else if(filterContext.Exception is NotLoggedInException)
                HandleAuthCookieError(filterContext);
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

        private void HandleAuthCookieError(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 302;
            filterContext.HttpContext.Response.RedirectLocation = new HomeUrl().Relative;
            ClearAllCookies(filterContext);
            base.OnException(filterContext);
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