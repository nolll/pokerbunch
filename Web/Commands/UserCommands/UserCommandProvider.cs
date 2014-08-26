using Application.Services;
using Core.Repositories;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Commands.UserCommands
{
    public class UserCommandProvider : IUserCommandProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordSender _passwordSender;
        private readonly IUserService _userService;
        private readonly IRegistrationConfirmationSender _registrationConfirmationSender;
        private readonly IAuth _auth;

        public UserCommandProvider(
            IUserRepository userRepository,
            IPasswordSender passwordSender,
            IUserService userService,
            IRegistrationConfirmationSender registrationConfirmationSender,
            IAuth auth)
        {
            _userRepository = userRepository;
            _passwordSender = passwordSender;
            _userService = userService;
            _registrationConfirmationSender = registrationConfirmationSender;
            _auth = auth;
        }

        public Command GetForgotPasswordCommand(ForgotPasswordPostModel postModel)
        {
            return new ForgotPasswordCommand(
                _userRepository,
                _passwordSender,
                postModel);
        }

        public Command GetChangePasswordCommand(ChangePasswordPostModel postModel)
        {
            var user = _auth.CurrentUser;

            return new ChangePasswordCommand(
                _userRepository,
                user, 
                postModel);
        }

        public Command GetEditCommand(string userName, EditUserPostModel postModel)
        {
            var user = _userRepository.GetByNameOrEmail(userName);

            return new EditUserCommand(
                _userRepository,
                user, 
                postModel);
        }

        public Command GetAddCommand(AddUserPostModel postModel)
        {
            return new AddUserCommand(
                _userService,
                _userRepository,
                _registrationConfirmationSender,
                postModel);
        }
    }
}