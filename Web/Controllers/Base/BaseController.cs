using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.ErrorModels;
using Web.Security;

namespace Web.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected AppSettings AppSettings { get; }
        private IUserIdentity _identity;

        protected BaseController(AppSettings appSettings)
        {
            AppSettings = appSettings;
        }

        protected IUserIdentity Identity => _identity ?? (_identity = AppSettings.UseFakeData ? (IUserIdentity)new FakeUserIdentity() : new UserIdentity(User));

        protected ActionResult Error404()
        {
            return ShowError(new Error404PageModel(AppSettings));
        }

        protected ActionResult Error401()
        {
            return ShowError(new Error401PageModel(AppSettings));
        }

        protected ActionResult Error403()
        {
            return ShowError(new Error403PageModel(AppSettings));
        }

        protected ActionResult Error500()
        {
            return ShowError(new Error500PageModel(AppSettings));
        }

        protected ActionResult ErrorAuthCookie()
        {
            return ShowError(new ErrorAuthCookiePageModel(AppSettings));
        }

        private ActionResult ShowError(ErrorPageModel model)
        {
            return View(model);
        }

        protected ActionResult JsonView(object data)
        {
            return new JsonResult(data);
        }

        protected ViewResult View(IViewModel model)
        {
            return View(model.GetView(), model);
        }
    }
}