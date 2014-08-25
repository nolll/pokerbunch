using Application.Services;
using Core.Repositories;
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
        private readonly IRegistrationConfirmationSender _registrationConfirmationSender;
        private readonly AddUserPostModel _postModel;

        public AddUserCommand(
            IUserService userService,
            IUserModelMapper userModelMapper,
            IUserRepository userRepository,
            IPasswordGenerator passwordGenerator,
            ISaltGenerator saltGenerator,
            IRegistrationConfirmationSender registrationConfirmationSender,
            AddUserPostModel postModel)
        {
            _userService = userService;
            _userModelMapper = userModelMapper;
            _userRepository = userRepository;
            _passwordGenerator = passwordGenerator;
            _saltGenerator = saltGenerator;
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
            var password = _passwordGenerator.CreatePassword();
            var salt = _saltGenerator.CreateSalt();
            var encryptedPassword = EncryptionService.Encrypt(password, salt);
            var user = _userModelMapper.GetUser(_postModel, encryptedPassword, salt);
            _userRepository.Add(user);
            _registrationConfirmationSender.Send(user, password);
            return true;
        }
    }
}