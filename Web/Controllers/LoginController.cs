using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core.Exceptions;
using Core.UseCases.Login;
using Core.UseCases.LoginForm;
using Web.Controllers.Base;
using Web.Models.AuthModels;

namespace Web.Controllers
{
    public class LoginController : PokerBunchController
    {
        private const int AuthVersion = 2;

        [Route("-/auth/login")]
        public ActionResult Login(string returnUrl = null)
        {
            return ShowForm(returnUrl);
        }

        [HttpPost]
        [Route("-/auth/login")]
        public ActionResult Post(LoginPostModel postModel)
        {
            var request = new LoginRequest(postModel.LoginName, postModel.Password);

            try
            {
                var result = UseCase.Login.Execute(request);
                SignIn(result.UserName, postModel.RememberMe);
                return Redirect(postModel.ReturnUrl);
            }
            catch (LoginException ex)
            {
                AddModelError(ex.Message);
            }

            return ShowForm(postModel);
        }

        public void SignIn(string userName, bool createPersistentCookie)
        {
            var currentTime = DateTime.UtcNow;
            var expires = currentTime.AddYears(100);

            var authTicket = new FormsAuthenticationTicket(
                AuthVersion,
                userName,
                currentTime,
                expires,
                createPersistentCookie,
                "");

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

        private ActionResult ShowForm(string returnUrl, LoginPostModel postModel = null)
        {
            var contextResult = GetAppContext();
            var loginFormResult = UseCase.LoginForm.Execute(new LoginFormRequest(returnUrl));
            var model = new LoginPageModel(contextResult, loginFormResult, postModel);
            return View("~/Views/Login/Login.cshtml", model);
        }

        private ActionResult ShowForm(LoginPostModel postModel)
        {
            return ShowForm(postModel.ReturnUrl, postModel);
        }
    }
}