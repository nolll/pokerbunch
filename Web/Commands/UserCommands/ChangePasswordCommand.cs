using Core.Classes;
using Core.Repositories;
using Core.Services;
using Web.Models.UserModels.ChangePassword;

namespace Web.Commands.UserCommands
{
    public class ChangePasswordCommand : Command
    {
        private readonly ISaltGenerator _saltGenerator;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserRepository _userRepository;
        private readonly User _user;
        private readonly ChangePasswordPostModel _postModel;

        public ChangePasswordCommand(
            ISaltGenerator saltGenerator,
            IEncryptionService encryptionService,
            IUserRepository userRepository,
            User user, 
            ChangePasswordPostModel postModel)
        {
            _saltGenerator = saltGenerator;
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _user = user;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (IsValid(_postModel))
            {
                return false;
            }
            if (_postModel.Password != _postModel.Repeat)
            {
                AddError("The passwords does not match");
                return false;
            }
            var salt = _saltGenerator.CreateSalt();
            var encryptedPassword = _encryptionService.Encrypt(_postModel.Password, salt);
            _userRepository.SetEncryptedPassword(_user, encryptedPassword);
            _userRepository.SetSalt(_user, salt);
            return true;
        }
    }
}