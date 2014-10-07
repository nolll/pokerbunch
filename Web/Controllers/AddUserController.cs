using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.AddUser;
using Web.Models.UserModels.Add;

namespace Web.Controllers
{
    public class AddUserController : ControllerBase
    {
        [Route("-/user/add")]
        public ActionResult AddUser()
        {
            return GetForm();
        }

        [HttpPost]
        [Route("-/user/add")]
        public ActionResult Post(AddUserPostModel postModel)
        {
            try
            {
                var request = new AddUserRequest(postModel.UserName, postModel.DisplayName, postModel.Email);
                var result = UseCase.AddUser(request);
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
            
            return GetForm(postModel);
        }

        private ActionResult GetForm(AddUserPostModel postModel = null)
        {
            var contextResult = UseCase.AppContext();
            var model = new AddUserPageModel(contextResult, postModel);
            return View("AddUser/AddUser", model);
        }

        [Route("-/user/created")]
        public ActionResult Done()
        {
            var contextResult = UseCase.AppContext();
            var model = new AddUserConfirmationPageModel(contextResult);
            return View("AddUser/AddUserDone", model);
        }
    }
}