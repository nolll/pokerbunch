using Application.Services;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Commands.UserCommands
{
    public class ForgotPasswordCommand : Command
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordSender _passwordSender;
        private readonly ForgotPasswordPostModel _postModel;
        
        public ForgotPasswordCommand(
            IUserRepository userRepository,
            IPasswordSender passwordSender,
            ForgotPasswordPostModel postModel)
        {
            _userRepository = userRepository;
            _passwordSender = passwordSender;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel))
            {
                return false;
            }
            var user = _userRepository.GetByNameOrEmail(_postModel.Email);
            if (user == null)
            {
                return false;
            }
            var password = PasswordGenerator.CreatePassword();
            var salt = SaltGenerator.CreateSalt();
            var encryptedPassword = EncryptionService.Encrypt(password, salt);
            var changedUser = UserModelMapper.GetUser(user, encryptedPassword, salt);
            _userRepository.Save(changedUser);
            _passwordSender.Send(changedUser, password);
            return true;
        }
    }
}