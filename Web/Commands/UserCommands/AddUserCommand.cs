using Application.Services;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.UserModels.Add;

namespace Web.Commands.UserCommands
{
    public class AddUserCommand : Command
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IRegistrationConfirmationSender _registrationConfirmationSender;
        private readonly AddUserPostModel _postModel;

        public AddUserCommand(
            IUserService userService,
            IUserRepository userRepository,
            IRegistrationConfirmationSender registrationConfirmationSender,
            AddUserPostModel postModel)
        {
            _userService = userService;
            _userRepository = userRepository;
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
            var password = PasswordGenerator.CreatePassword();
            var salt = SaltGenerator.CreateSalt();
            var encryptedPassword = EncryptionService.Encrypt(password, salt);
            var user = UserModelMapper.GetUser(_postModel, encryptedPassword, salt);
            _userRepository.Add(user);
            _registrationConfirmationSender.Send(user, password);
            return true;
        }
    }
}