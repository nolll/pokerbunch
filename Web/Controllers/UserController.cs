using System.Web.Mvc;
using Application.Services;
using Core.Classes;
using Web.Commands.UserCommands;
using Web.ModelServices;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;
using Web.Services;

namespace Web.Controllers
{
	public class UserController : ControllerBase
    {
	    private readonly IAuthentication _authentication;
	    private readonly IUrlProvider _urlProvider;
	    private readonly IUserCommandProvider _userCommandProvider;
	    private readonly IUserModelService _userModelService;

	    public UserController(
            IAuthentication authentication,
            IUrlProvider urlProvider,
            IUserCommandProvider userCommandProvider,
            IUserModelService userModelService)
	    {
	        _authentication = authentication;
	        _urlProvider = urlProvider;
	        _userCommandProvider = userCommandProvider;
	        _userModelService = userModelService;
	    }

		public ActionResult Details(string userName)
        {
			_authentication.RequireUser();
			var model = _userModelService.GetDetailsModel(userName); 
			return View("Details", model);
		}

        [AuthorizeRole(Role = Role.Admin)]
        public ActionResult List()
        {
            var model = _userModelService.GetListModel();
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
                return Redirect(_urlProvider.GetUserAddConfirmationUrl());
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

        public ActionResult Edit(string userName)
        {
			_authentication.RequireUser();
            var model = _userModelService.GetEditModel(userName);
            return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string userName, EditUserPostModel postModel)
        {
			_authentication.RequireUser();
			var command = _userCommandProvider.GetEditCommand(userName, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetUserDetailsUrl(userName));
            }
            AddModelErrors(command.Errors);
            var model = _userModelService.GetEditModel(userName, postModel);
            return View("Edit/Edit", model);
		}

        public ActionResult ChangePassword()
        {
			_authentication.RequireUser();
            var model = _userModelService.GetChangePasswordModel();
			return View("ChangePassword/ChangePassword", model);
		}

        [HttpPost]
		public ActionResult ChangePassword(ChangePasswordPostModel postModel)
        {
			_authentication.RequireUser();
            var command = _userCommandProvider.GetChangePasswordCommand(postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetChangePasswordConfirmationUrl());
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
                return Redirect(_urlProvider.GetForgotPasswordConfirmationUrl());
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