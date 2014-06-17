using Application.Services;
using Core.Repositories;
using Web.ModelFactories.UserModelFactories;
using Web.Models.UserModels;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;

namespace Web.ModelServices
{
    public class UserModelService : IUserModelService
    {
        private readonly IAuth _auth;
        private readonly IUserRepository _userRepository;
        private readonly IUserDetailsPageBuilder _userDetailsPageBuilder;
        private readonly IAddUserPageBuilder _addUserPageBuilder;
        private readonly IAddUserConfirmationPageBuilder _addUserConfirmationPageBuilder;
        private readonly IEditUserPageBuilder _editUserPageBuilder;
        private readonly IChangePasswordPageBuilder _changePasswordPageBuilder;
        private readonly IForgotPasswordPageBuilder _forgotPasswordPageBuilder;

        public UserModelService(
            IAuth auth,
            IUserRepository userRepository,
            IUserDetailsPageBuilder userDetailsPageBuilder,
            IAddUserPageBuilder addUserPageBuilder,
            IAddUserConfirmationPageBuilder addUserConfirmationPageBuilder,
            IEditUserPageBuilder editUserPageBuilder,
            IChangePasswordPageBuilder changePasswordPageBuilder,
            IForgotPasswordPageBuilder forgotPasswordPageBuilder)
        {
            _auth = auth;
            _userRepository = userRepository;
            _userDetailsPageBuilder = userDetailsPageBuilder;
            _addUserPageBuilder = addUserPageBuilder;
            _addUserConfirmationPageBuilder = addUserConfirmationPageBuilder;
            _editUserPageBuilder = editUserPageBuilder;
            _changePasswordPageBuilder = changePasswordPageBuilder;
            _forgotPasswordPageBuilder = forgotPasswordPageBuilder;
        }

        public UserDetailsPageModel GetDetailsModel(string userName)
        {
            var currentUser = _auth.CurrentUser;
            var displayUser = _userRepository.GetByNameOrEmail(userName);
            return _userDetailsPageBuilder.Build(currentUser, displayUser);
        }

        public AddUserPageModel GetAddModel(AddUserPostModel postModel)
        {
            return _addUserPageBuilder.Build(postModel);
        }

        public AddUserConfirmationPageModel GetAddConfirmationModel()
        {
            return _addUserConfirmationPageBuilder.Build();
        }

        public EditUserPageModel GetEditModel(string userName, EditUserPostModel postModel)
        {
            var user = _userRepository.GetByNameOrEmail(userName);
            return _editUserPageBuilder.Build(user, postModel);
        }

        public ChangePasswordPageModel GetChangePasswordModel()
        {
            return _changePasswordPageBuilder.Build(); 
        }

        public ChangePasswordConfirmationPageModel GetChangePasswordConfirmationModel()
        {
            return _changePasswordPageBuilder.BuildConfirmation();
        }

        public ForgotPasswordPageModel GetForgotPasswordModel(ForgotPasswordPostModel postModel)
        {
            return _forgotPasswordPageBuilder.Build(postModel);
        }

        public ForgotPasswordConfirmationPageModel GetForgotPasswordConfirmationModel()
        {
            return _forgotPasswordPageBuilder.BuildConfirmation();
        }
    }
}