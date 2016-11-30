using System.Web.Mvc;
using System.Web.Security;
using Web.Controllers.Base;
using Web.Routes;
using Web.Urls.SiteUrls;

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