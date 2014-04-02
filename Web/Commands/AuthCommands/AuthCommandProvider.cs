using Application.Services;
using Core.Repositories;
using Web.Models.AuthModels;
using Web.Services;

namespace Web.Commands.AuthCommands
{
    public class AuthCommandProvider : IAuthCommandProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IWebContext _webContext;
        private readonly IFormsAuthenticationService _formsAuthenticationService;
        private readonly IHomegameRepository _homegameRepository;

        public AuthCommandProvider(
            IUserRepository userRepository,
            IEncryptionService encryptionService,
            IWebContext webContext,
            IFormsAuthenticationService formsAuthenticationService,
            IHomegameRepository homegameRepository)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _webContext = webContext;
            _formsAuthenticationService = formsAuthenticationService;
            _homegameRepository = homegameRepository;
        }

        public Command GetLoginCommand(AuthLoginPostModel postModel)
        {
            return new LoginCommand(
                _userRepository,
                _encryptionService,
                _webContext,
                _formsAuthenticationService,
                _homegameRepository,
                postModel);
        }

        public Command GetLogoutCommand()
        {
            return new LogoutCommand(
                _webContext,
                _formsAuthenticationService);
        }
    }
}