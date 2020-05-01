using System.Threading.Tasks;
using Core.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class LogoutController : BaseController
    {
        public LogoutController(AppSettings appSettings)
            : base(appSettings)
        {
        }

        [HttpPost]
        [Route(LogoutUrl.Route)]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}