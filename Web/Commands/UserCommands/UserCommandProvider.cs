using Core.Classes;
using Core.Repositories;
using Core.Services;
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
        private readonly IEncryptionService _encryptionService;
        private readonly IPasswordSender _passwordSender;
        private readonly IUserModelMapper _userModelMapper;
        private readonly IUserService _userService;
        private readonly IRegistrationConfirmationSender _registrationConfirmationSender;

        public UserCommandProvider(
            IUserRepository userRepository,
            IPasswordGenerator passwordGenerator,
            ISaltGenerator saltGenerator,
            IEncryptionService encryptionService,
            IPasswordSender passwordSender,
            IUserModelMapper userModelMapper,
            IUserService userService,
            IRegistrationConfirmationSender registrationConfirmationSender)
        {
            _userRepository = userRepository;
            _passwordGenerator = passwordGenerator;
            _saltGenerator = saltGenerator;
            _encryptionService = encryptionService;
            _passwordSender = passwordSender;
            _userModelMapper = userModelMapper;
            _userService = userService;
            _registrationConfirmationSender = registrationConfirmationSender;
        }

        public Command GetForgotPasswordCommand(ForgotPasswordPostModel postModel)
        {
            return new ForgotPasswordCommand(
                _userRepository,
                _passwordGenerator,
                _saltGenerator,
                _encryptionService,
                _passwordSender,
                _userModelMapper,
                postModel);
        }

        public Command GetChangePasswordCommand(User user, ChangePasswordPostModel postModel)
        {
            return new ChangePasswordCommand(
                _saltGenerator,
                _encryptionService,
                _userRepository,
                _userModelMapper,
                user, 
                postModel);
        }

        public Command GetEditCommand(User user, EditUserPostModel postModel)
        {
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
                _encryptionService,
                _registrationConfirmationSender,
                postModel);
        }
    }
}