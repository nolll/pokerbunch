using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.AppModels.Add;
using Web.Urls;

namespace Web.Controllers
{
    public class AddAppController : BaseController
    {
        [Route(WebRoutes.AppAdd)]
        [Authorize]
        public ActionResult AddUser()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(WebRoutes.AppAdd)]
        [Authorize]
        public ActionResult Post(AddAppPostModel postModel)
        {
            try
            {
                var request = new AddApp.Request(CurrentUserName, postModel.AppName);
                UseCase.AddApp.Execute(request);
                return Redirect(new UserAppsUrl().Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            
            return ShowForm(postModel);
        }

        private ActionResult ShowForm(AddAppPostModel postModel = null)
        {
            var contextResult = GetAppContext();
            var model = new AddAppPageModel(contextResult, postModel);
            return View("~/Views/Pages/AddApp/AddApp.cshtml", model);
        }
    }
}