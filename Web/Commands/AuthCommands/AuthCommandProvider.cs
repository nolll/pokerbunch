using Application.Services;
using Core.Repositories;
using Web.Models.AuthModels;
using Web.Security;
using Web.Services;

namespace Web.Commands.AuthCommands
{
    public class AuthCommandProvider : IAuthCommandProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IAuth _auth;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;

        public AuthCommandProvider(
            IUserRepository userRepository,
            IEncryptionService encryptionService,
            IAuth auth,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _auth = auth;
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
        }

        public Command GetLoginCommand(AuthLoginPostModel postModel)
        {
            return new LoginCommand(
                _userRepository,
                _encryptionService,
                _auth,
                _homegameRepository,
                _playerRepository,
                postModel);
        }

        public Command GetLogoutCommand()
        {
            return new LogoutCommand(
                _auth);
        }
    }
}