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
        private readonly IWebContext _webContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHomegameRepository _homegameRepository;

        public AuthCommandProvider(
            IUserRepository userRepository,
            IEncryptionService encryptionService,
            IWebContext webContext,
            IAuthenticationService authenticationService,
            IHomegameRepository homegameRepository)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _webContext = webContext;
            _authenticationService = authenticationService;
            _homegameRepository = homegameRepository;
        }

        public Command GetLoginCommand(AuthLoginPostModel postModel)
        {
            return new LoginCommand(
                _userRepository,
                _encryptionService,
                _webContext,
                _authenticationService,
                _homegameRepository,
                postModel);
        }

        public Command GetLogoutCommand()
        {
            return new LogoutCommand(
                _webContext,
                _authenticationService);
        }
    }
}