using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.EditUserForm;
using Application.UseCases.UserDetails;
using Web.Commands.UserCommands;
using Web.Models.UserModels;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;
using Web.Models.UserModels.List;
using Web.Security.Attributes;

namespace Web.Controllers
{
	public class UserController : ControllerBase
    {
	    private readonly IUserCommandProvider _userCommandProvider;

	    public UserController(
            IUserCommandProvider userCommandProvider)
	    {
	        _userCommandProvider = userCommandProvider;
	    }

        [Authorize]
        [Route("-/user/details/{userName}")]
        public ActionResult Details(string userName)
        {
            var contextResult = UseCase.AppContext();
            var userDetailsResult = UseCase.UserDetails(new UserDetailsRequest(userName));
            var model = new UserDetailsPageModel(contextResult, userDetailsResult);
			return View("Details", model);
		}

        [AuthorizeAdmin]
        [Route("-/user/list")]
        public ActionResult List()
        {
            var contextResult = UseCase.AppContext();
            var showUserListResult = UseCase.UserList();
            var model = new UserListPageModel(contextResult, showUserListResult);
			return View("List/List", model);
		}

        [Route("-/user/add")]
        public ActionResult Add()
        {
            var model = BuildAddModel();
			return View("Add/Add", model);
		}

        [HttpPost]
        [Route("-/user/add")]
        public ActionResult Add_Post(AddUserPostModel postModel)
        {
            var command = _userCommandProvider.GetAddCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new AddUserConfirmationUrl().Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildAddModel(postModel);
            return View("Add/Add", model);
		}

	    [Route("-/user/created")]
        public ActionResult Created()
		{
            var contextResult = UseCase.AppContext();
            var model = new AddUserConfirmationPageModel(contextResult);
			return View("Add/Confirmation", model);
		}

	    [Authorize]
        [Route("-/user/edit/{userName}")]
        public ActionResult Edit(string userName)
        {
            var model = BuildEditModel(userName);
            return View("Edit/Edit", model);
		}

	    [HttpPost]
        [Authorize]
        [Route("-/user/edit/{userName}")]
        public ActionResult Edit_Post(string userName, EditUserPostModel postModel)
        {
			var command = _userCommandProvider.GetEditCommand(userName, postModel);
            if (command.Execute())
            {
                return Redirect(new UserDetailsUrl(userName).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildEditModel(userName, postModel);
            return View("Edit/Edit", model);
		}

	    [Authorize]
        [Route("-/user/changepassword")]
        public ActionResult ChangePassword()
        {
            var model = BuildChangePasswordModel();
			return View("ChangePassword/ChangePassword", model);
		}

	    [HttpPost]
        [Authorize]
        [Route("-/user/changepassword")]
        public ActionResult ChangePassword_Post(ChangePasswordPostModel postModel)
        {
            var command = _userCommandProvider.GetChangePasswordCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new ChangePasswordConfirmationUrl().Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildChangePasswordModel();
            return View("ChangePassword/ChangePassword", model);
		}

	    [Route("-/user/changedpassword")]
        public ActionResult ChangedPassword()
        {
            var contextResult = UseCase.AppContext();
            var model = new ChangePasswordConfirmationPageModel(contextResult);
			return View("ChangePassword/Confirmation", model);
		}

	    [Route("-/user/forgotpassword")]
        public ActionResult ForgotPassword()
        {
            var model = BuildForgotPasswordModel();
			return View("ForgotPassword/ForgotPassword", model);
		}

	    [HttpPost]
        [Route("-/user/forgotpassword")]
        public ActionResult ForgotPassword_Post(ForgotPasswordPostModel postModel)
        {
            var command = _userCommandProvider.GetForgotPasswordCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new ForgotPasswordConfirmationUrl().Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildForgotPasswordModel(postModel);
            return View("ForgotPassword/ForgotPassword", model);
		}

	    [Route("-/user/passwordsent")]
        public ActionResult PasswordSent()
		{
            var contextResult = UseCase.AppContext();
            var model = new ForgotPasswordConfirmationPageModel(contextResult);
			return View("ForgotPassword/Confirmation", model);
		}

	    private AddUserPageModel BuildAddModel(AddUserPostModel postModel = null)
	    {
            var contextResult = UseCase.AppContext();
	        return new AddUserPageModel(contextResult, postModel);
	    }

	    private ChangePasswordPageModel BuildChangePasswordModel()
	    {
            var contextResult = UseCase.AppContext();
	        return new ChangePasswordPageModel(contextResult);
	    }

	    private ForgotPasswordPageModel BuildForgotPasswordModel(ForgotPasswordPostModel postModel = null)
	    {
            var contextResult = UseCase.AppContext();
	        return new ForgotPasswordPageModel(contextResult, postModel);
	    }

	    private EditUserPageModel BuildEditModel(string userName, EditUserPostModel postModel = null)
	    {
            var contextResult = UseCase.AppContext();
            var editUserFormResult = UseCase.EditUserForm(new EditUserFormRequest(userName));
	        return new EditUserPageModel(contextResult, editUserFormResult, postModel);
	    }
    }
}