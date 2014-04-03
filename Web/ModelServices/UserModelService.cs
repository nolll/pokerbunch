using Core.Repositories;
using Core.UseCases;
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
        private readonly IShowUserList _showUserList;

        public UserModelService(
            IAuthentication authentication,
            IUserRepository userRepository,
            IUserDetailsPageModelFactory userDetailsPageModelFactory,
            IUserListPageModelFactory userListPageModelFactory,
            IAddUserPageModelFactory addUserPageModelFactory,
            IAddUserConfirmationPageModelFactory addUserConfirmationPageModelFactory,
            IEditUserPageModelFactory editUserPageModelFactory,
            IChangePasswordPageModelFactory changePasswordPageModelFactory,
            IForgotPasswordPageModelFactory forgotPasswordPageModelFactory,
            IShowUserList showUserList)
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
            _showUserList = showUserList;
        }

        public UserDetailsPageModel GetDetailsModel(string userName)
        {
            var currentUser = _authentication.GetUser();
            var displayUser = _userRepository.GetByNameOrEmail(userName);
            return _userDetailsPageModelFactory.Create(currentUser, displayUser);
        }

        public UserListPageModel GetListModel()
        {
            var showUserListResult = _showUserList.Execute();
            return _userListPageModelFactory.Create(showUserListResult);
        }

        public AddUserPageModel GetAddModel(AddUserPostModel postModel)
        {
            return _addUserPageModelFactory.Create(postModel);
        }

        public AddUserConfirmationPageModel GetAddConfirmationModel()
        {
            return _addUserConfirmationPageModelFactory.Create();
        }

        public EditUserPageModel GetEditModel(string userName, EditUserPostModel postModel)
        {
            var user = _userRepository.GetByNameOrEmail(userName);
            return _editUserPageModelFactory.Create(user, postModel);
        }

        public ChangePasswordPageModel GetChangePasswordModel()
        {
            return _changePasswordPageModelFactory.Create(); 
        }

        public ChangePasswordConfirmationPageModel GetChangePasswordConfirmationModel()
        {
            return _changePasswordPageModelFactory.CreateConfirmation();
        }

        public ForgotPasswordPageModel GetForgotPasswordModel(ForgotPasswordPostModel postModel)
        {
            return _forgotPasswordPageModelFactory.Create(postModel);
        }

        public ForgotPasswordConfirmationPageModel GetForgotPasswordConfirmationModel()
        {
            return _forgotPasswordPageModelFactory.CreateConfirmation();
        }
    }
}