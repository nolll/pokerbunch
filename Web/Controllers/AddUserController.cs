using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.UserModels.Add;
using Web.Urls;

namespace Web.Controllers
{
    public class AddUserController : BaseController
    {
        [Route(WebRoutes.UserAdd)]
        public ActionResult AddUser()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(WebRoutes.UserAdd)]
        public ActionResult Post(AddUserPostModel postModel)
        {
            try
            {
                var request = new AddUser.Request(postModel.UserName, postModel.DisplayName, postModel.Email, new LoginUrl().Absolute);
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

        [Route(WebRoutes.UserAddConfirmation)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new AddUserConfirmationPageModel(contextResult);
            return View("~/Views/Pages/AddUser/AddUserDone.cshtml", model);
        }
    }
}