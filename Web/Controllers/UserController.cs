using System.Web.Mvc;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Web.Commands.UserCommands;
using Web.ModelFactories.UserModelFactories;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Controllers
{
	public class UserController : ControllerBase
    {
	    private readonly IAuthentication _authentication;
        private readonly IUserRepository _userRepository;
        private readonly IUserDetailsPageModelFactory _userDetailsPageModelFactory;
	    private readonly IUserListPageModelFactory _userListPageModelFactory;
	    private readonly IAddUserPageModelFactory _addUserPageModelFactory;
	    private readonly IAddUserConfirmationPageModelFactory _addUserConfirmationPageModelFactory;
	    private readonly IEditUserPageModelFactory _editUserPageModelFactory;
	    private readonly IChangePasswordPageModelFactory _changePasswordPageModelFactory;
	    private readonly IForgotPasswordPageModelFactory _forgotPasswordPageModelFactory;
	    private readonly IUrlProvider _urlProvider;
	    private readonly IUserCommandProvider _userCommandProvider;

	    public UserController(
            IAuthentication authentication,
            IUserRepository userRepository,
            IUserDetailsPageModelFactory userDetailsPageModelFactory,
            IUserListPageModelFactory userListPageModelFactory,
            IAddUserPageModelFactory addUserPageModelFactory,
            IAddUserConfirmationPageModelFactory addUserConfirmationPageModelFactory,
            IEditUserPageModelFactory editUserPageModelFactory,
            IChangePasswordPageModelFactory changePasswordPageModelFactory,
            IForgotPasswordPageModelFactory forgotPasswordPageModelFactory,
            IUrlProvider urlProvider,
            IUserCommandProvider userCommandProvider)
	    {
	        _authentication = authentication;
            _userRepository = userRepository;
	        _userDetailsPageModelFactory = userDetailsPageModelFactory;
	        _userListPageModelFactory = userListPageModelFactory;
	        _addUserPageModelFactory = addUserPageModelFactory;
	        _addUserConfirmationPageModelFactory = addUserConfirmationPageModelFactory;
	        _editUserPageModelFactory = editUserPageModelFactory;
	        _changePasswordPageModelFactory = changePasswordPageModelFactory;
	        _forgotPasswordPageModelFactory = forgotPasswordPageModelFactory;
	        _urlProvider = urlProvider;
	        _userCommandProvider = userCommandProvider;
	    }

		public ActionResult Details(string name)
        {
			_authentication.RequireUser();
			var user = _userRepository.GetByNameOrEmail(name);
			if(name == null){
				throw new UserNotFoundException();
			}
			var model = _userDetailsPageModelFactory.Create(_authentication.GetUser(), user);
			return View("Details", model);
		}

        public ActionResult List()
        {
            _authentication.RequireUser();
            _authentication.RequireAdmin();
			var users = _userRepository.GetList();
			var model = _userListPageModelFactory.Create(_authentication.GetUser(), users);

			return View("List/List", model);
		}

        public ActionResult Add()
        {
			var model = _addUserPageModelFactory.Create(_authentication.GetUser());
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
            var model = _addUserPageModelFactory.Create(_authentication.GetUser(), postModel);
            return View("Add/Add", model);
		}

		public ActionResult Created()
		{
		    var model = _addUserConfirmationPageModelFactory.Create(_authentication.GetUser());
			return View("Add/Confirmation", model);
		}

        public ActionResult Edit(string name)
        {
			_authentication.RequireUser();
			var user = _userRepository.GetByNameOrEmail(name);
			if(user == null){
				throw new UserNotFoundException();
			}
            var model = _editUserPageModelFactory.Create(user);
            return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string name, EditUserPostModel postModel)
        {
			_authentication.RequireUser();
			var user = _userRepository.GetByNameOrEmail(name);
            var command = _userCommandProvider.GetEditCommand(user, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetUserDetailsUrl(user));
            }
            AddModelErrors(command.Errors);
            var model = _editUserPageModelFactory.Create(user, postModel);
            return View("Edit/Edit", model);
		}

        public ActionResult ChangePassword()
        {
			_authentication.RequireUser();
            var model = _changePasswordPageModelFactory.Create(_authentication.GetUser()); 
			return View("ChangePassword/ChangePassword", model);
		}

        [HttpPost]
		public ActionResult ChangePassword(ChangePasswordPostModel postModel)
        {
			_authentication.RequireUser();
			var user = _authentication.GetUser();
            var command = _userCommandProvider.GetChangePasswordCommand(user, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetChangePasswordConfirmationUrl());
            }
            AddModelErrors(command.Errors);
            var model = _changePasswordPageModelFactory.Create(_authentication.GetUser());
            return View("ChangePassword/ChangePassword", model);
		}

		public ActionResult ChangedPassword()
        {
            var model = _changePasswordPageModelFactory.CreateConfirmation(_authentication.GetUser());
			return View("ChangePassword/Confirmation", model);
		}

        public ActionResult ForgotPassword()
        {
            var model = _forgotPasswordPageModelFactory.Create(_authentication.GetUser());
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
            var model = _forgotPasswordPageModelFactory.Create(_authentication.GetUser(), postModel);
            return View("ForgotPassword/ForgotPassword", model);
		}

		public ActionResult PasswordSent()
        {
			var model = _forgotPasswordPageModelFactory.CreateConfirmation(_authentication.GetUser());
			return View("ForgotPassword/Confirmation", model);
		}

	}
}