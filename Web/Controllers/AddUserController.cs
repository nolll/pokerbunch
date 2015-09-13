using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.UserModels.Add;
using Web.Urls;

namespace Web.Controllers
{
    public class AddUserController : BaseController
    {
        [Route(Routes.UserAdd)]
        public ActionResult AddUser()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(Routes.UserAdd)]
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

        [Route(Routes.UserAddConfirmation)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new AddUserConfirmationPageModel(contextResult);
            return View("~/Views/Pages/AddUser/AddUserDone.cshtml", model);
        }
    }
}