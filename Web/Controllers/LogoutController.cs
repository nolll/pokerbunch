using System.Web.Mvc;
using System.Web.Security;
using Core.Urls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class LogoutController : PokerBunchController
    {
        [Route("-/auth/logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(new HomeUrl().Relative);
        }
    }
}