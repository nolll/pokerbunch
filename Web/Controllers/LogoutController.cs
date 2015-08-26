using System.Web.Mvc;
using System.Web.Security;
using Web.Controllers.Base;
using Web.Urls;

namespace Web.Controllers
{
    public class LogoutController : BaseController
    {
        [Route(Routes.AuthLogout)]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(new HomeUrl().Relative);
        }
    }
}