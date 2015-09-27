using System.Web.Mvc;
using System.Web.Security;
using Web.Common.Routes;
using Web.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class LogoutController : BaseController
    {
        [Route(WebRoutes.AuthLogout)]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(new HomeUrl().Relative);
        }
    }
}