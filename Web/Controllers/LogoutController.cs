using System;
using System.Web.Mvc;
using System.Web.Security;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class LogoutController : BaseController
    {
        [Route(LogoutUrl.Route)]
        public ActionResult Logout()
        {
            DeleteTokenCookie();
            FormsAuthentication.SignOut();
            return Redirect(new HomeUrl().Relative);
        }

        private void DeleteTokenCookie()
        {
            var cookie = Response.Cookies["token"];
            if (cookie != null)
                cookie.Expires = DateTime.Now.AddDays(-1);
        }
    }
}