using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Extensions;
using Web.Models.UserModels.Add;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class AddUserController : BaseController
    {
        [Route(WebRoutes.User.Add)]
        public ActionResult AddUser()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(WebRoutes.User.Add)]
        public ActionResult Post(AddUserPostModel postModel)
        {
            try
            {
                var request = new AddUser.Request(postModel.UserName, postModel.DisplayName, postModel.Email, postModel.Password);
                UseCase.AddUser.Execute(request);
                return Redirect(new AddUserConfirmationUrl().Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            catch (UserExistsException ex)
            {
                AddModelError(ex.Message);
            }
            catch (EmailExistsException ex)
            {
                AddModelError(ex.Message);
            }
            
            return ShowForm(postModel);
        }

        private ActionResult ShowForm(AddUserPostModel postModel = null)
        {
            var contextResult = GetAppContext();
            var model = new AddUserPageModel(contextResult, postModel);
            return View("~/Views/Pages/AddUser/AddUser.cshtml", model);
        }

        [Route(WebRoutes.User.AddConfirmation)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new AddUserConfirmationPageModel(contextResult);
            return View("~/Views/Pages/AddUser/AddUserDone.cshtml", model);
        }
    }
}