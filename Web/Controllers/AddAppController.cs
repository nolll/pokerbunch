using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.AppModels.Add;

namespace Web.Controllers
{
    public class AddAppController : CoreController
    {
        private readonly AddApp _addApp;

        public AddAppController(AppSettings appSettings, CoreContext coreContext, AddApp addApp)
            : base(appSettings, coreContext)
        {
            _addApp = addApp;
        }

        [Route(AddAppUrl.Route)]
        [Authorize]
        public ActionResult AddApp()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(AddAppUrl.Route)]
        [Authorize]
        public ActionResult Post(AddAppPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddApp.Request(postModel.AppName);
                _addApp.Execute(request);
                return Redirect(new UserAppsUrl().Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }
            
            return ShowForm(postModel, errors);
        }

        private ActionResult ShowForm(AddAppPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var model = new AddAppPageModel(AppSettings, contextResult, postModel, errors);
            return View(model);
        }
    }
}