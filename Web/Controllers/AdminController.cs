using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.AdminModels;

namespace Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly TestEmail _testEmail;
        private readonly ClearCache _clearCache;

        public AdminController(AppSettings appSettings, TestEmail testEmail, ClearCache clearCache) 
            : base(appSettings)
        {
            _testEmail = testEmail;
            _clearCache = clearCache;
        }

        [Authorize]
        [Route(TestEmailUrl.Route)]
        public ActionResult SendEmail()
        {
            var result = _testEmail.Execute();
            var model = new EmailModel(result);
            return View(model);
        }

        [Authorize]
        [Route(ClearCacheUrl.Route)]
        public ActionResult ClearCache()
        {
            var result = _clearCache.Execute();
            var model = new ClearCacheModel(result.Message);
            return View(model);
        }
    }
}
