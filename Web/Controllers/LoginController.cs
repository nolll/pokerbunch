using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Routes;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.AuthModels;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        private const int AuthVersion = 2;

        [Route(WebRoutes.Auth.Login)]
        public ActionResult Login(string returnUrl = null)
        {
            return ShowForm(returnUrl);
        }

        [HttpPost]
        [Route(WebRoutes.Auth.Login)]
        public ActionResult Post(LoginPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new Login.Request(postModel.LoginName, postModel.Password);
                var result = UseCase.Login.Execute(request);
                SignIn(result.UserName, result.Token, postModel.RememberMe);
                return Redirect(postModel.ReturnUrl);
            }
            catch (LoginException ex)
            {
                errors.Add(ex.Message);
            }

            return ShowForm(postModel, errors);
        }

        public void SignIn(string userName, string token, bool createPersistentCookie)
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

        private ActionResult ShowForm(string returnUrl, LoginPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var loginFormResult = UseCase.LoginForm.Execute(new LoginForm.Request(new HomeUrl().Relative, returnUrl));
            var model = new LoginPageModel(contextResult, loginFormResult, postModel, errors);
            return View(model);
        }

        private ActionResult ShowForm(LoginPostModel postModel, IEnumerable<string> errors = null)
        {
            return ShowForm(postModel.ReturnUrl, postModel, errors);
        }
    }
}