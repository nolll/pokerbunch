using System.Web.Mvc;
using Core.Urls;
using Core.UseCases.EditUserForm;
using Web.Commands.UserCommands;
using Web.Controllers.Base;
using Web.Models.UserModels.Edit;

namespace Web.Controllers
{
    public class EditUserController : PokerBunchController
    {
        private readonly IUserCommandProvider _userCommandProvider;

        public EditUserController(
            IUserCommandProvider userCommandProvider)
        {
            _userCommandProvider = userCommandProvider;
        }

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
            var command = _userCommandProvider.GetEditCommand(userName, postModel);
            if (command.Execute())
            {
                return Redirect(new UserDetailsUrl(userName).Relative);
            }
            AddModelErrors(command.Errors);
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