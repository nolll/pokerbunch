using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.AppContext;
using Application.UseCases.UserDetails;
using Application.UseCases.UserList;
using Web.Commands.UserCommands;
using Web.ModelFactories.UserModelFactories;
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
	    private readonly IAppContextInteractor _appContextInteractor;
	    private readonly IUserDetailsInteractor _userDetailsInteractor;
	    private readonly IUserListInteractor _userListInteractor;
	    private readonly IUserCommandProvider _userCommandProvider;
	    private readonly IEditUserPageBuilder _editUserPageBuilder;

	    public UserController(
            IAppContextInteractor appContextInteractor,
            IUserDetailsInteractor userDetailsInteractor,
            IUserListInteractor userListInteractor,
            IUserCommandProvider userCommandProvider,
            IEditUserPageBuilder editUserPageBuilder)
	    {
	        _appContextInteractor = appContextInteractor;
	        _userDetailsInteractor = userDetailsInteractor;
	        _userListInteractor = userListInteractor;
	        _userCommandProvider = userCommandProvider;
	        _editUserPageBuilder = editUserPageBuilder;
	    }

        [Authorize]
		public ActionResult Details(string userName)
        {
            var contextResult = _appContextInteractor.Execute();
            var userDetailsResult = _userDetailsInteractor.Execute(new UserDetailsRequest(userName));
            var model = new UserDetailsPageModel(contextResult, userDetailsResult);
			return View("Details", model);
		}

        [AuthorizeAdmin]
        public ActionResult List()
        {
            var contextResult = _appContextInteractor.Execute();
            var showUserListResult = _userListInteractor.Execute();
            var model = new UserListPageModel(contextResult, showUserListResult);
			return View("List/List", model);
		}

        public ActionResult Add()
        {
            var model = BuildAddModel();
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
            var model = BuildAddModel(postModel);
            return View("Add/Add", model);
		}

	    private AddUserPageModel BuildAddModel(AddUserPostModel postModel = null)
	    {
	        var contextResult = _appContextInteractor.Execute();
            return new AddUserPageModel(contextResult, postModel);
	    }

		public ActionResult Created()
		{
            var contextResult = _appContextInteractor.Execute();
            var model = new AddUserConfirmationPageModel(contextResult);
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
            var model = BuildChangePasswordModel();
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
            var model = BuildChangePasswordModel();
            return View("ChangePassword/ChangePassword", model);
		}

	    private ChangePasswordPageModel BuildChangePasswordModel()
	    {
            var contextResult = _appContextInteractor.Execute();
            return new ChangePasswordPageModel(contextResult);
	    }

		public ActionResult ChangedPassword()
        {
            var contextResult = _appContextInteractor.Execute();
            var model = new ChangePasswordConfirmationPageModel(contextResult);
			return View("ChangePassword/Confirmation", model);
		}

        public ActionResult ForgotPassword()
        {
            var model = BuildForgotPasswordModel();
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
            var model = BuildForgotPasswordModel(postModel);
            return View("ForgotPassword/ForgotPassword", model);
		}

        public ForgotPasswordPageModel BuildForgotPasswordModel(ForgotPasswordPostModel postModel = null)
	    {
            var contextResult = _appContextInteractor.Execute();
            return new ForgotPasswordPageModel(contextResult, postModel);
	    }

		public ActionResult PasswordSent()
		{
            var contextResult = _appContextInteractor.Execute();
            var model = new ForgotPasswordConfirmationPageModel(contextResult);
			return View("ForgotPassword/Confirmation", model);
		}

	}
}