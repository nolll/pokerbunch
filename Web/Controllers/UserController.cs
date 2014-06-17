using System.Web.Mvc;
using Application.Urls;
using Web.Commands.UserCommands;
using Web.ModelFactories.UserModelFactories;
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
	    private readonly IUserListPageBuilder _userListPageBuilder;
	    private readonly IUserDetailsPageBuilder _userDetailsPageBuilder;
	    private readonly IAddUserPageBuilder _addUserPageBuilder;
	    private readonly IAddUserConfirmationPageBuilder _addUserConfirmationPageBuilder;
	    private readonly IEditUserPageBuilder _editUserPageBuilder;
	    private readonly IChangePasswordPageBuilder _changePasswordPageBuilder;
	    private readonly IForgotPasswordPageBuilder _forgotPasswordPageBuilder;

	    public UserController(
            IUserCommandProvider userCommandProvider,
            IUserListPageBuilder userListPageBuilder,
            IUserDetailsPageBuilder userDetailsPageBuilder,
            IAddUserPageBuilder addUserPageBuilder,
            IAddUserConfirmationPageBuilder addUserConfirmationPageBuilder,
            IEditUserPageBuilder editUserPageBuilder,
            IChangePasswordPageBuilder changePasswordPageBuilder,
            IForgotPasswordPageBuilder forgotPasswordPageBuilder)
	    {
	        _userCommandProvider = userCommandProvider;
	        _userListPageBuilder = userListPageBuilder;
	        _userDetailsPageBuilder = userDetailsPageBuilder;
	        _addUserPageBuilder = addUserPageBuilder;
	        _addUserConfirmationPageBuilder = addUserConfirmationPageBuilder;
	        _editUserPageBuilder = editUserPageBuilder;
	        _changePasswordPageBuilder = changePasswordPageBuilder;
	        _forgotPasswordPageBuilder = forgotPasswordPageBuilder;
	    }

        [Authorize]
		public ActionResult Details(string userName)
        {
			var model = _userDetailsPageBuilder.Build(userName);
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
            var model = _addUserPageBuilder.Build();
			return View("Add/Add", model);
		}

        [HttpPost]
		public ActionResult Add(AddUserPostModel postModel)
        {
            var command = _userCommandProvider.GetAddCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new AddUserConfirmationUrl().Relative);
            }
            AddModelErrors(command.Errors);
            var model = _addUserPageBuilder.Build(postModel);
            return View("Add/Add", model);
		}

		public ActionResult Created()
		{
		    var model = _addUserConfirmationPageBuilder.Build();
			return View("Add/Confirmation", model);
		}

        [Authorize]
        public ActionResult Edit(string userName)
        {
            var model = _editUserPageBuilder.Build(userName);
            return View("Edit/Edit", model);
		}

        [HttpPost]
        [Authorize]
        public ActionResult Edit(string userName, EditUserPostModel postModel)
        {
			var command = _userCommandProvider.GetEditCommand(userName, postModel);
            if (command.Execute())
            {
                return Redirect(new UserDetailsUrl(userName).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _editUserPageBuilder.Build(userName, postModel);
            return View("Edit/Edit", model);
		}

        [Authorize]
        public ActionResult ChangePassword()
        {
            var model = _changePasswordPageBuilder.Build();
			return View("ChangePassword/ChangePassword", model);
		}

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordPostModel postModel)
        {
            var command = _userCommandProvider.GetChangePasswordCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new ChangePasswordConfirmationUrl().Relative);
            }
            AddModelErrors(command.Errors);
            var model = _changePasswordPageBuilder.Build();
            return View("ChangePassword/ChangePassword", model);
		}

		public ActionResult ChangedPassword()
        {
            var model = _changePasswordPageBuilder.BuildConfirmation();
			return View("ChangePassword/Confirmation", model);
		}

        public ActionResult ForgotPassword()
        {
            var model = _forgotPasswordPageBuilder.Build();
			return View("ForgotPassword/ForgotPassword", model);
		}

        [HttpPost]
		public ActionResult ForgotPassword(ForgotPasswordPostModel postModel)
        {
            var command = _userCommandProvider.GetForgotPasswordCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new ForgotPasswordConfirmationUrl().Relative);
            }
            AddModelErrors(command.Errors);
            var model = _forgotPasswordPageBuilder.Build(postModel);
            return View("ForgotPassword/ForgotPassword", model);
		}

		public ActionResult PasswordSent()
		{
            var model = _forgotPasswordPageBuilder.BuildConfirmation();
			return View("ForgotPassword/Confirmation", model);
		}

	}
}