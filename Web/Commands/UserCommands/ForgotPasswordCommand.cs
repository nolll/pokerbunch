using App.Services.Interfaces;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Commands.UserCommands
{
    public class ForgotPasswordCommand : Command
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly ISaltGenerator _saltGenerator;
        private readonly IEncryptionService _encryptionService;
        private readonly IPasswordSender _passwordSender;
        private readonly IUserModelMapper _userModelMapper;
        private readonly ForgotPasswordPostModel _postModel;
        
        public ForgotPasswordCommand(
            IUserRepository userRepository,
            IPasswordGenerator passwordGenerator,
            ISaltGenerator saltGenerator,
            IEncryptionService encryptionService,
            IPasswordSender passwordSender,
            IUserModelMapper userModelMapper,
            ForgotPasswordPostModel postModel)
        {
            _userRepository = userRepository;
            _passwordGenerator = passwordGenerator;
            _saltGenerator = saltGenerator;
            _encryptionService = encryptionService;
            _passwordSender = passwordSender;
            _userModelMapper = userModelMapper;
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
            var password = _passwordGenerator.CreatePassword();
            var salt = _saltGenerator.CreateSalt();
            var encryptedPassword = _encryptionService.Encrypt(password, salt);
            var changedUser = _userModelMapper.GetUser(user, encryptedPassword, salt);
            _userRepository.Save(changedUser);
            _passwordSender.Send(changedUser, password);
            return true;
        }
    }
}