using System.Web.Mvc;
using Application.Urls;
using Web.Commands.UserCommands;
using Web.Models.UserModels.Add;

namespace Web.Controllers
{
    public class AddUserController : ControllerBase
    {
        private readonly IUserCommandProvider _userCommandProvider;

        public AddUserController(
            IUserCommandProvider userCommandProvider)
        {
            _userCommandProvider = userCommandProvider;
        }

        [Route("-/user/add")]
        public ActionResult AddUser()
        {
            return GetForm();
        }

        [HttpPost]
        [Route("-/user/add")]
        public ActionResult Post(AddUserPostModel postModel)
        {
            var command = _userCommandProvider.GetAddCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new AddUserConfirmationUrl().Relative);
            }
            AddModelErrors(command.Errors);
            return GetForm(postModel);
        }

        private ActionResult GetForm(AddUserPostModel postModel = null)
        {
            var contextResult = UseCase.AppContext();
            var model = new AddUserPageModel(contextResult, postModel);
            return View("~/Views/Pages/AddUser/AddUser.cshtml", model);
        }

        [Route("-/user/created")]
        public ActionResult Done()
        {
            var contextResult = UseCase.AppContext();
            var model = new AddUserConfirmationPageModel(contextResult);
            return View("~/Views/Pages/AddUser/AddUserDone.cshtml", model);
        }
    }
}