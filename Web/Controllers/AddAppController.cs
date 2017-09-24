using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Routes;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.AppModels.Add;

namespace Web.Controllers
{
    public class AddAppController : BaseController
    {
        [Route(WebRoutes.App.Add)]
        [Authorize]
        public ActionResult AddUser()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(WebRoutes.App.Add)]
        [Authorize]
        public ActionResult Post(AddAppPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddApp.Request(postModel.AppName);
                UseCase.AddApp.Execute(request);
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
            var model = new AddAppPageModel(contextResult, postModel, errors);
            return View(model);
        }
    }
}