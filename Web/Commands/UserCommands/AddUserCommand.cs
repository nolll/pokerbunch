using Core.Repositories;
using Core.Services;
using Web.ModelMappers;
using Web.Models.UserModels.Add;

namespace Web.Commands.UserCommands
{
    public class AddUserCommand : Command
    {
        private readonly IUserService _userService;
        private readonly IUserModelMapper _userModelMapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly ISaltGenerator _saltGenerator;
        private readonly IEncryptionService _encryptionService;
        private readonly IRegistrationConfirmationSender _registrationConfirmationSender;
        private readonly AddUserPostModel _postModel;

        public AddUserCommand(
            IUserService userService,
            IUserModelMapper userModelMapper,
            IUserRepository userRepository,
            IPasswordGenerator passwordGenerator,
            ISaltGenerator saltGenerator,
            IEncryptionService encryptionService,
            IRegistrationConfirmationSender registrationConfirmationSender,
            AddUserPostModel postModel)
        {
            _userService = userService;
            _userModelMapper = userModelMapper;
            _userRepository = userRepository;
            _passwordGenerator = passwordGenerator;
            _saltGenerator = saltGenerator;
            _encryptionService = encryptionService;
            _registrationConfirmationSender = registrationConfirmationSender;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            if (!_userService.IsUserNameAvailable(_postModel.UserName))
            {
                AddError("The User Name is already in use");
            }
            if (!_userService.IsEmailAvailable(_postModel.Email))
            {
                AddError("The Email Address is already in use");
            }
            if (HasErrors)
            {
                return false;
            }
            var user = _userModelMapper.GetUser(_postModel);
            _userRepository.AddUser(user);
            var password = _passwordGenerator.CreatePassword();
            var salt = _saltGenerator.CreateSalt();
            var encryptedPassword = _encryptionService.Encrypt(password, salt);
            var token = _encryptionService.Encrypt(_postModel.UserName, salt);
            _userRepository.SetToken(user, token);
            _userRepository.SetEncryptedPassword(user, encryptedPassword);
            _userRepository.SetSalt(user, salt);
            _registrationConfirmationSender.Send(user, password);
            return true;
        }
    }
}