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
        private readonly IAuthentication _authentication;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;

        public AuthCommandProvider(
            IUserRepository userRepository,
            IEncryptionService encryptionService,
            IAuthentication authentication,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _authentication = authentication;
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
        }

        public Command GetLoginCommand(AuthLoginPostModel postModel)
        {
            return new LoginCommand(
                _userRepository,
                _encryptionService,
                _authentication,
                _homegameRepository,
                _playerRepository,
                postModel);
        }

        public Command GetLogoutCommand()
        {
            return new LogoutCommand(
                _authentication);
        }
    }
}