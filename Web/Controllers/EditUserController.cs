using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.EditUser;
using Core.UseCases.EditUserForm;
using Web.Controllers.Base;
using Web.Models.UserModels.Edit;

namespace Web.Controllers
{
    public class EditUserController : PokerBunchController
    {
        [Authorize]
        [Route("-/user/edit/{userName}")]
        public ActionResult EditUser(string userName)
        {
            return ShowForm(userName);
        }

        [HttpPost]
        [Authorize]
        [Route("-/user/edit/{userName}")]
        public ActionResult Post(string userName, EditUserPostModel postModel)
        {
            try
            {
                var request = new EditUserRequest(userName, postModel.DisplayName, postModel.RealName, postModel.Email);
                var result = UseCase.EditUser(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(userName, postModel);
        }

        private ActionResult ShowForm(string userName, EditUserPostModel postModel = null)
        {
            var contextResult = UseCase.AppContext();
            var editUserFormResult = UseCase.EditUserForm(new EditUserFormRequest(userName));
            var model = new EditUserPageModel(contextResult, editUserFormResult, postModel);
            return View("~/Views/Pages/EditUser/EditUser.cshtml", model);
        }
    }
}