using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Services.Interfaces;
using Web.ModelMappers;
using Web.Models.UserModels.ChangePassword;

namespace Web.Commands.UserCommands
{
    public class ChangePasswordCommand : Command
    {
        private readonly IUserRepository _userRepository;
        private readonly IRandomService _randomService;
        private readonly User _user;
        private readonly ChangePasswordPostModel _postModel;

        public ChangePasswordCommand(
            IUserRepository userRepository,
            IRandomService randomService,
            User user, 
            ChangePasswordPostModel postModel)
        {
            _userRepository = userRepository;
            _randomService = randomService;
            _user = user;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel))
            {
                return false;
            }
            if (_postModel.Password != _postModel.Repeat)
            {
                AddError("The passwords does not match");
                return false;
            }
            var salt = SaltGenerator.CreateSalt(_randomService.GetAllowedChars());
            var encryptedPassword = EncryptionService.Encrypt(_postModel.Password, salt);
            var user = UserModelMapper.GetUser(_user, encryptedPassword, salt);
            _userRepository.Save(user);
            return true;
        }
    }
}