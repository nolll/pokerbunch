using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.AddUser;
using Web.Controllers.Base;
using Web.Models.UserModels.Add;

namespace Web.Controllers
{
    public class AddUserController : BaseController
    {
        [Route("-/user/add")]
        public ActionResult AddUser()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route("-/user/add")]
        public ActionResult Post(AddUserPostModel postModel)
        {
            try
            {
                var request = new AddUserRequest(postModel.UserName, postModel.DisplayName, postModel.Email);
                var result = UseCase.AddUser.Execute(request);
                return Redirect(result.ReturnUrl.Relative);
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

        [Route("-/user/created")]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new AddUserConfirmationPageModel(contextResult);
            return View("~/Views/Pages/AddUser/AddUserDone.cshtml", model);
        }
    }
}