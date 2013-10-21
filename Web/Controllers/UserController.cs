using System.Web.Mvc;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Web.ModelFactories.UserModelFactories;
using Web.ModelMappers;
using Web.Models.UrlModels;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Controllers{

	public class UserController : Controller {
	    private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IUserDetailsPageModelFactory _userDetailsPageModelFactory;
	    private readonly IUserListingPageModelFactory _userListingPageModelFactory;
	    private readonly IUserService _userService;
	    private readonly IPasswordGenerator _passwordGenerator;
	    private readonly ISaltGenerator _saltGenerator;
	    private readonly IEncryptionService _encryptionService;
	    private readonly IUserModelMapper _userModelMapper;
	    private readonly IRegistrationConfirmationSender _registrationConfirmationSender;
	    private readonly IAddUserPageModelFactory _addUserPageModelFactory;
	    private readonly IAddUserConfirmationPageModelFactory _addUserConfirmationPageModelFactory;
	    private readonly IEditUserPageModelFactory _editUserPageModelFactory;
	    private readonly IChangePasswordPageModelFactory _changePasswordPageModelFactory;
	    private readonly IForgotPasswordPageModelFactory _forgotPasswordPageModelFactory;
	    private readonly IPasswordSender _passwordSender;
	    private readonly IUrlProvider _urlProvider;

	    public UserController(
            IUserContext userContext,
            IUserRepository userRepository,
            IUserDetailsPageModelFactory userDetailsPageModelFactory,
            IUserListingPageModelFactory userListingPageModelFactory,
            IUserService userService,
            IPasswordGenerator passwordGenerator,
            ISaltGenerator saltGenerator,
            IEncryptionService encryptionService,
            IUserModelMapper userModelMapper,
            IRegistrationConfirmationSender registrationConfirmationSender,
            IAddUserPageModelFactory addUserPageModelFactory,
            IAddUserConfirmationPageModelFactory addUserConfirmationPageModelFactory,
            IEditUserPageModelFactory editUserPageModelFactory,
            IChangePasswordPageModelFactory changePasswordPageModelFactory,
            IForgotPasswordPageModelFactory forgotPasswordPageModelFactory,
            IPasswordSender passwordSender,
            IUrlProvider urlProvider)
	    {
	        _userContext = userContext;
            _userRepository = userRepository;
	        _userDetailsPageModelFactory = userDetailsPageModelFactory;
	        _userListingPageModelFactory = userListingPageModelFactory;
	        _userService = userService;
	        _passwordGenerator = passwordGenerator;
	        _saltGenerator = saltGenerator;
	        _encryptionService = encryptionService;
	        _userModelMapper = userModelMapper;
	        _registrationConfirmationSender = registrationConfirmationSender;
	        _addUserPageModelFactory = addUserPageModelFactory;
	        _addUserConfirmationPageModelFactory = addUserConfirmationPageModelFactory;
	        _editUserPageModelFactory = editUserPageModelFactory;
	        _changePasswordPageModelFactory = changePasswordPageModelFactory;
	        _forgotPasswordPageModelFactory = forgotPasswordPageModelFactory;
	        _passwordSender = passwordSender;
	        _urlProvider = urlProvider;
	    }

		public ActionResult Details(string name){
			_userContext.RequireUser();
			var user = _userRepository.GetUserByName(name);
			if(name == null){
				throw new UserNotFoundException();
			}
			var model = _userDetailsPageModelFactory.Create(_userContext.GetUser(), user);
			return View("Details", model);
		}

        public ActionResult Listing(){
			_userContext.RequireAdmin();
			var users = _userRepository.GetUsers();
			var model = _userListingPageModelFactory.Create(_userContext.GetUser(), users);

			return View("Listing/Listing", model);
		}

        public ActionResult Add(){
			var model = _addUserPageModelFactory.Create(_userContext.GetUser());
			return View("Add/Add", model);
		}

        [HttpPost]
		public ActionResult Add(AddUserPostModel postModel){
			if(ModelState.IsValid)
			{
			    var hasError = false;
                if (!_userService.IsUserNameAvailable(postModel.UserName))
                {
                    ModelState.AddModelError("username_inuse", "The User Name is already in use");
                    hasError = true;
                }
                if (!_userService.IsEmailAvailable(postModel.Email))
                {
                    ModelState.AddModelError("email_inuse", "The Email Address is already in use");
                    hasError = true;
                }
                if (!hasError)
                {
                    var user = _userModelMapper.GetUser(postModel);
                    _userRepository.AddUser(user);
				    var password = _passwordGenerator.CreatePassword();
				    var salt = _saltGenerator.CreateSalt();
				    var encryptedPassword = _encryptionService.Encrypt(password, salt);
			        var token = _encryptionService.Encrypt(postModel.UserName, salt);
				    _userRepository.SetToken(user, token);
				    _userRepository.SetEncryptedPassword(user, encryptedPassword);
				    _userRepository.SetSalt(user, salt);
				    _registrationConfirmationSender.Send(user, password);
				    return Redirect(_urlProvider.GetUserAddConfirmationUrl());
                }
			}
            var model = _addUserPageModelFactory.Create(_userContext.GetUser(), postModel);
			return View("Add/Add", model);
		}

		public ActionResult Created()
		{
		    var model = _addUserConfirmationPageModelFactory.Create(_userContext.GetUser());
			return View("Add/Confirmation", model);
		}

        public ActionResult Edit(string name){
			_userContext.RequireUser();
			var user = _userRepository.GetUserByName(name);
			if(user == null){
				throw new UserNotFoundException();
			}
            var model = _editUserPageModelFactory.Create(user);
            return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string name, EditUserPostModel postModel){
			_userContext.RequireUser();
			var user = _userRepository.GetUserByName(name);
            if(ModelState.IsValid){
				user = _userModelMapper.GetUser(user, postModel);
			    _userRepository.UpdateUser(user);
				return Redirect(_urlProvider.GetUserDetailsUrl(user));
			}
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
		public ActionResult ChangePassword(ChangePasswordPostModel postModel){
			_userContext.RequireUser();
			var user = _userContext.GetUser();
			if(ModelState.IsValid){
                if (postModel.Password == postModel.Repeat)
                {
                    var salt = _saltGenerator.CreateSalt();
    				var encryptedPassword = _encryptionService.Encrypt(postModel.Password, salt);
	    			_userRepository.SetEncryptedPassword(user, encryptedPassword);
		    		_userRepository.SetSalt(user, salt);
			    	return Redirect(_urlProvider.GetChangePasswordConfirmationUrl());
                }
                ModelState.AddModelError("password_mismatch", "The passwords does not match");
			}
            var model = _changePasswordPageModelFactory.Create(_userContext.GetUser()); 
			return View("ChangePassword/ChangePassword", model);
		}

		public ActionResult ChangedPassword(){
            var model = _changePasswordPageModelFactory.CreateConfirmation(_userContext.GetUser());
			return View("ChangePassword/Confirmation", model);
		}

        public ActionResult ForgotPassword()
        {
            var model = _forgotPasswordPageModelFactory.Create(_userContext.GetUser());
			return View("ForgotPassword/ForgotPassword", model);
		}

        [HttpPost]
		public ActionResult ForgotPassword(ForgotPasswordPostModel postModel){
    		if(ModelState.IsValid){
				var user = _userRepository.GetUserByEmail(postModel.Email);
				if(user != null){
					var password = _passwordGenerator.CreatePassword();
					var salt = _saltGenerator.CreateSalt();
			        var encryptedPassword = _encryptionService.Encrypt(password, salt);
			        _userRepository.SetEncryptedPassword(user, encryptedPassword);
			        _userRepository.SetSalt(user, salt);
					_passwordSender.Send(user, password);
				}
				return Redirect(_urlProvider.GetForgotPasswordConfirmationUrl());
			}
            var model = _forgotPasswordPageModelFactory.Create(_userContext.GetUser(), postModel);
			return View("ForgotPassword/ForgotPassword", model);
		}

		public ActionResult PasswordSent(){
			var model = _forgotPasswordPageModelFactory.CreateConfirmation(_userContext.GetUser());
			return View("ForgotPassword/Confirmation", model);
		}

	}
}