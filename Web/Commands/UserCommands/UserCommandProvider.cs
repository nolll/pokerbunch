using Application.Services;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Commands.UserCommands
{
    public class UserCommandProvider : IUserCommandProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly ISaltGenerator _saltGenerator;
        private readonly IPasswordSender _passwordSender;
        private readonly IUserModelMapper _userModelMapper;
        private readonly IUserService _userService;
        private readonly IRegistrationConfirmationSender _registrationConfirmationSender;
        private readonly IAuth _auth;

        public UserCommandProvider(
            IUserRepository userRepository,
            IPasswordGenerator passwordGenerator,
            ISaltGenerator saltGenerator,
            IPasswordSender passwordSender,
            IUserModelMapper userModelMapper,
            IUserService userService,
            IRegistrationConfirmationSender registrationConfirmationSender,
            IAuth auth)
        {
            _userRepository = userRepository;
            _passwordGenerator = passwordGenerator;
            _saltGenerator = saltGenerator;
            _passwordSender = passwordSender;
            _userModelMapper = userModelMapper;
            _userService = userService;
            _registrationConfirmationSender = registrationConfirmationSender;
            _auth = auth;
        }

        public Command GetForgotPasswordCommand(ForgotPasswordPostModel postModel)
        {
            return new ForgotPasswordCommand(
                _userRepository,
                _passwordGenerator,
                _saltGenerator,
                _passwordSender,
                _userModelMapper,
                postModel);
        }

        public Command GetChangePasswordCommand(ChangePasswordPostModel postModel)
        {
            var user = _auth.CurrentUser;

            return new ChangePasswordCommand(
                _saltGenerator,
                _userRepository,
                _userModelMapper,
                user, 
                postModel);
        }

        public Command GetEditCommand(string userName, EditUserPostModel postModel)
        {
            var user = _userRepository.GetByNameOrEmail(userName);

            return new EditUserCommand(
                _userModelMapper,
                _userRepository,
                user, 
                postModel);
        }

        public Command GetAddCommand(AddUserPostModel postModel)
        {
            return new AddUserCommand(
                _userService,
                _userModelMapper,
                _userRepository,
                _passwordGenerator,
                _saltGenerator,
                _registrationConfirmationSender,
                postModel);
        }
    }
}