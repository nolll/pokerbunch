using System.Web.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class LogoutController : PokerBunchController
    {
        [Route("-/auth/logout")]
        public ActionResult Logout()
        {
            var result = UseCase.Logout.Execute();
            return Redirect(result.ReturnUrl.Relative);
        }
    }
}