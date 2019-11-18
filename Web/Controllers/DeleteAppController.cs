using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteAppController : BaseController
    {
        private readonly DeleteApp _deleteApp;

        public DeleteAppController(AppSettings appSettings, DeleteApp deleteApp) 
            : base(appSettings)
        {
            _deleteApp = deleteApp;
        }

        [Authorize]
        [Route(DeleteAppUrl.Route)]
        public ActionResult Delete(string appId)
        {
            var request = new DeleteApp.Request(appId);
            _deleteApp.Execute(request);
            var returnUrl = new UserAppsUrl();
            return Redirect(returnUrl.Relative);
        }
    }
}