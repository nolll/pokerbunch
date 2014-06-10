using System.Web.Mvc;
using Web.Commands.UserCommands;
using Web.ModelFactories.UserModelFactories;
using Web.ModelServices;
using Web.Models.UrlModels;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;
using Web.Security.Attributes;

namespace Web.Controllers
{
	public class UserController : ControllerBase
    {
	    private readonly IUserCommandProvider _userCommandProvider;
	    private readonly IUserModelService _userModelService;
	    private readonly IUserListPageBuilder _userListPageBuilder;

	    public UserController(
            IUserCommandProvider userCommandProvider,
            IUserModelService userModelService,
            IUserListPageBuilder userListPageBuilder)
	    {
	        _userCommandProvider = userCommandProvider;
	        _userModelService = userModelService;
	        _userListPageBuilder = userListPageBuilder;
	    }

        [Authorize]
		public ActionResult Details(string userName)
        {
			var model = _userModelService.GetDetailsModel(userName); 
			return View("Details", model);
		}

        [AuthorizeAdmin]
        public ActionResult List()
        {
            var model = _userListPageBuilder.Build();
			return View("List/List", model);
		}

        public ActionResult Add()
        {
            var model = _userModelService.GetAddModel();
			return View("Add/Add", model);
		}

        [HttpPost]
		public ActionResult Add(AddUserPostModel postModel)
        {
            var command = _userCommandProvider.GetAddCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new AddUserConfirmationUrlModel().Relative);
            }
            AddModelErrors(command.Errors);
            var model = _userModelService.GetAddModel(postModel);
            return View("Add/Add", model);
		}

		public ActionResult Created()
		{
		    var model = _userModelService.GetAddConfirmationModel();
			return View("Add/Confirmation", model);
		}

        [Authorize]
        public ActionResult Edit(string userName)
        {
            var model = _userModelService.GetEditModel(userName);
            return View("Edit/Edit", model);
		}

        [HttpPost]
        [Authorize]
        public ActionResult Edit(string userName, EditUserPostModel postModel)
        {
			var command = _userCommandProvider.GetEditCommand(userName, postModel);
            if (command.Execute())
            {
                return Redirect(new UserDetailsUrlModel(userName).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _userModelService.GetEditModel(userName, postModel);
            return View("Edit/Edit", model);
		}

        [Authorize]
        public ActionResult ChangePassword()
        {
            var model = _userModelService.GetChangePasswordModel();
			return View("ChangePassword/ChangePassword", model);
		}

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordPostModel postModel)
        {
            var command = _userCommandProvider.GetChangePasswordCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new ChangePasswordConfirmationUrlModel().Relative);
            }
            AddModelErrors(command.Errors);
            var model = _userModelService.GetChangePasswordModel();
            return View("ChangePassword/ChangePassword", model);
		}

		public ActionResult ChangedPassword()
        {
            var model = _userModelService.GetChangePasswordConfirmationModel();
			return View("ChangePassword/Confirmation", model);
		}

        public ActionResult ForgotPassword()
        {
            var model = _userModelService.GetForgotPasswordModel();
			return View("ForgotPassword/ForgotPassword", model);
		}

        [HttpPost]
		public ActionResult ForgotPassword(ForgotPasswordPostModel postModel)
        {
            var command = _userCommandProvider.GetForgotPasswordCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new ForgotPasswordConfirmationUrlModel().Relative);
            }
            AddModelErrors(command.Errors);
            var model = _userModelService.GetForgotPasswordModel(postModel);
            return View("ForgotPassword/ForgotPassword", model);
		}

		public ActionResult PasswordSent()
		{
		    var model = _userModelService.GetForgotPasswordConfirmationModel();
			return View("ForgotPassword/Confirmation", model);
		}

	}
}