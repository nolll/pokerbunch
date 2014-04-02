using Core.Repositories;
using Web.ModelFactories.UserModelFactories;
using Web.Models.UserModels;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;
using Web.Models.UserModels.List;
using Web.Security;

namespace Web.ModelServices
{
    public class UserModelService : IUserModelService
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

        public UserModelService(
            IAuthentication authentication,
            IUserRepository userRepository,
            IUserDetailsPageModelFactory userDetailsPageModelFactory,
            IUserListPageModelFactory userListPageModelFactory,
            IAddUserPageModelFactory addUserPageModelFactory,
            IAddUserConfirmationPageModelFactory addUserConfirmationPageModelFactory,
            IEditUserPageModelFactory editUserPageModelFactory,
            IChangePasswordPageModelFactory changePasswordPageModelFactory,
            IForgotPasswordPageModelFactory forgotPasswordPageModelFactory)
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
        }

        public UserDetailsPageModel GetDetailsModel(string userName)
        {
            var currentUser = _authentication.GetUser();
            var displayUser = _userRepository.GetByNameOrEmail(userName);
            return _userDetailsPageModelFactory.Create(currentUser, displayUser);
        }

        public UserListPageModel GetListModel()
        {
            var currentUser = _authentication.GetUser();
            var users = _userRepository.GetList();
            return _userListPageModelFactory.Create(currentUser, users);
        }

        public AddUserPageModel GetAddModel(AddUserPostModel postModel)
        {
            var currentUser = _authentication.GetUser();
            return _addUserPageModelFactory.Create(currentUser, postModel);
        }

        public AddUserConfirmationPageModel GetAddConfirmationModel()
        {
            var currentUser = _authentication.GetUser();
            return _addUserConfirmationPageModelFactory.Create(currentUser);
        }

        public EditUserPageModel GetEditModel(string userName, EditUserPostModel postModel)
        {
            var user = _userRepository.GetByNameOrEmail(userName);
            return _editUserPageModelFactory.Create(user, postModel);
        }

        public ChangePasswordPageModel GetChangePasswordModel()
        {
            var currentUser = _authentication.GetUser();
            return _changePasswordPageModelFactory.Create(currentUser); 
        }

        public ChangePasswordConfirmationPageModel GetChangePasswordConfirmationModel()
        {
            var currentUser = _authentication.GetUser();
            return _changePasswordPageModelFactory.CreateConfirmation(currentUser);
        }

        public ForgotPasswordPageModel GetForgotPasswordModel(ForgotPasswordPostModel postModel)
        {
            var currentUser = _authentication.GetUser();
            return _forgotPasswordPageModelFactory.Create(currentUser, postModel);
        }

        public ForgotPasswordConfirmationPageModel GetForgotPasswordConfirmationModel()
        {
            var currentUser = _authentication.GetUser();
            return _forgotPasswordPageModelFactory.CreateConfirmation(currentUser);
        }
    }
}