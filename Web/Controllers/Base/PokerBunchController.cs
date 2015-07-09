using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;
using Core;
using Core.Entities;
using Core.Exceptions;
using Core.Services;
using Core.UseCases.AppContext;
using Core.UseCases.BaseContext;
using Core.UseCases.BunchContext;
using Core.UseCases.CashgameContext;
using Core.UseCases.RequireAdmin;
using Core.UseCases.RequireManager;
using Core.UseCases.RequirePlayer;
using Web.Models.ErrorModels;
using Web.Plumbing;
using Web.Security;

namespace Web.Controllers.Base
{
    public class PokerBunchController : Controller
    {
        protected UseCaseContainer UseCase
        {
            get { return new UseCaseContainer(); }
        }

        private BaseContextResult GetBaseContext()
        {
            return UseCase.BaseContext.Execute();
        }
        
        protected AppContextResult GetAppContext()
        {
            return UseCase.AppContext.Execute(new AppContextRequest(UserName));
        }

        protected BunchContextResult GetBunchContext(string slug = null)
        {
            return UseCase.BunchContext.Execute(new BunchContextRequest(UserName, slug));
        }

        protected CashgameContextResult GetCashgameContext(string slug, DateTime currentTime, CashgamePage selectedPage = CashgamePage.Unknown, int? year = null)
        {
            return UseCase.CashgameContext.Execute(new CashgameContextRequest(UserName, slug, currentTime, selectedPage, year));
        }

        protected CustomIdentity Identity
        {
            get
            {
                var identity = User.Identity as CustomIdentity;
                return identity ?? new CustomIdentity();
            }
        }

        protected string UserName
        {
            get
            {
                if (!User.Identity.IsAuthenticated)
                    return null;
                return User.Identity.Name;
            }
        }

        protected IIdentity Identity1
        {
            get
            {
                return User.Identity;
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

        protected bool IsPlayer(string slug)
        {
            return Authorize.Bunch(User, slug, Role.Player);
        }

        protected bool IsPlayer(string slug, int playerId)
        {
            return Authorize.SpecificPlayer(User, slug, playerId);
        }

        protected bool IsManager(string slug)
        {
            return Authorize.Bunch(User, slug, Role.Manager);
        }

        protected void RequirePlayer(string slug)
        {
            UseCase.RequirePlayer.Execute(new RequirePlayerRequest(slug, Identity1.Name));
        }

        protected void RequireManager(string slug)
        {
            UseCase.RequireManager.Execute(new RequireManagerRequest(slug, Identity1.Name));
        }

        protected void RequireAdmin()
        {
            UseCase.RequireAdmin.Execute(new RequireAdminRequest(Identity1.Name));
        }
    }
}