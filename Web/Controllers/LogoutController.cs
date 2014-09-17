using System.Web.Mvc;

namespace Web.Controllers
{
    public class LogoutController : ControllerBase
    {
        [Route("-/auth/logout")]
        public ActionResult Logout()
        {
            var result = UseCase.Logout();
            return Redirect(result.ReturnUrl.Relative);
        }
    }
}