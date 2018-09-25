using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.AuthModels;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        private const int AuthVersion = 2;

        [Route(LoginUrl.Route)]
        public ActionResult Login(string returnUrl = null)
        {
            var contextResult = GetAppContext();
            var model = new LoginPageModel(contextResult);
            return View(model);
        }

        [HttpPost]
        [Route(LoginUrl.Route)]
        public ActionResult Login(LoginPostModel postModel)
        {
            try
            {
                var request = new Login.Request(postModel.Username, postModel.Password);
                var result = UseCase.Login.Execute(request);
                SignIn(result.UserName, result.Token, postModel.RememberMe);
                return JsonView(new JsonViewModelOk());
            }
            catch (LoginException ex)
            {
                return JsonView(new JsonViewModelError(ex.Message));
            }
        }

        private void SignIn(string userName, string token, bool createPersistentCookie)
        {
            var currentTime = DateTime.UtcNow;
            var expires = currentTime.AddDays(365);

            var authTicket = new FormsAuthenticationTicket(
                AuthVersion,
                userName,
                currentTime,
                expires,
                createPersistentCookie,
                token);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
            {
                Expires = authTicket.Expiration,
                Path = FormsAuthentication.FormsCookiePath
            };

            if (System.Web.HttpContext.Current != null)
            {
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}