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
	    private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IUserDetailsPageModelFactory _userDetailsPageModelFactory;
	    private readonly IUserListingPageModelFactory _userListingPageModelFactory;
	    private readonly IAddUserPageModelFactory _addUserPageModelFactory;
	    private readonly IAddUserConfirmationPageModelFactory _addUserConfirmationPageModelFactory;
	    private readonly IEditUserPageModelFactory _editUserPageModelFactory;
	    private readonly IChangePasswordPageModelFactory _changePasswordPageModelFactory;
	    private readonly IForgotPasswordPageModelFactory _forgotPasswordPageModelFactory;
	    private readonly IUrlProvider _urlProvider;
	    private readonly IUserCommandProvider _userCommandProvider;

	    public UserController(
            IUserContext userContext,
            IUserRepository userRepository,
            IUserDetailsPageModelFactory userDetailsPageModelFactory,
            IUserListingPageModelFactory userListingPageModelFactory,
            IAddUserPageModelFactory addUserPageModelFactory,
            IAddUserConfirmationPageModelFactory addUserConfirmationPageModelFactory,
            IEditUserPageModelFactory editUserPageModelFactory,
            IChangePasswordPageModelFactory changePasswordPageModelFactory,
            IForgotPasswordPageModelFactory forgotPasswordPageModelFactory,
            IUrlProvider urlProvider,
            IUserCommandProvider userCommandProvider)
	    {
	        _userContext = userContext;
            _userRepository = userRepository;
	        _userDetailsPageModelFactory = userDetailsPageModelFactory;
	        _userListingPageModelFactory = userListingPageModelFactory;
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
			_userContext.RequireUser();
			var user = _userRepository.GetUserByNameOrEmail(name);
			if(name == null){
				throw new UserNotFoundException();
			}
			var model = _userDetailsPageModelFactory.Create(_userContext.GetUser(), user);
			return View("Details", model);
		}

        public ActionResult Listing()
        {
			_userContext.RequireAdmin();
			var users = _userRepository.GetAll();
			var model = _userListingPageModelFactory.Create(_userContext.GetUser(), users);

			return View("Listing/Listing", model);
		}

        public ActionResult Add()
        {
			var model = _addUserPageModelFactory.Create(_userContext.GetUser());
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
            var model = _addUserPageModelFactory.Create(_userContext.GetUser(), postModel);
            return View("Add/Add", model);
		}

		public ActionResult Created()
		{
		    var model = _addUserConfirmationPageModelFactory.Create(_userContext.GetUser());
			return View("Add/Confirmation", model);
		}

        public ActionResult Edit(string name)
        {
			_userContext.RequireUser();
			var user = _userRepository.GetUserByNameOrEmail(name);
			if(user == null){
				throw new UserNotFoundException();
			}
            var model = _editUserPageModelFactory.Create(user);
            return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string name, EditUserPostModel postModel)
        {
			_userContext.RequireUser();
			var user = _userRepository.GetUserByNameOrEmail(name);
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
			_userContext.RequireUser();
            var model = _changePasswordPageModelFactory.Create(_userContext.GetUser()); 
			return View("ChangePassword/ChangePassword", model);
		}

        [HttpPost]
		public ActionResult ChangePassword(ChangePasswordPostModel postModel)
        {
			_userContext.RequireUser();
			var user = _userContext.GetUser();
            var command = _userCommandProvider.GetChangePasswordCommand(user, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetChangePasswordConfirmationUrl());
            }
            AddModelErrors(command.Errors);
            var model = _changePasswordPageModelFactory.Create(_userContext.GetUser());
            return View("ChangePassword/ChangePassword", model);
		}

		public ActionResult ChangedPassword()
        {
            var model = _changePasswordPageModelFactory.CreateConfirmation(_userContext.GetUser());
			return View("ChangePassword/Confirmation", model);
		}

        public ActionResult ForgotPassword()
        {
            var model = _forgotPasswordPageModelFactory.Create(_userContext.GetUser());
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
            var model = _forgotPasswordPageModelFactory.Create(_userContext.GetUser(), postModel);
            return View("ForgotPassword/ForgotPassword", model);
		}

		public ActionResult PasswordSent()
        {
			var model = _forgotPasswordPageModelFactory.CreateConfirmation(_userContext.GetUser());
			return View("ForgotPassword/Confirmation", model);
		}

	}
}