using System.Web.Mvc;
using System.Web.Security;
using PokerBunch.Common.Routes;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class LogoutController : BaseController
    {
        [Route(WebRoutes.Auth.Logout)]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(new HomeUrl().Relative);
        }
    }
}