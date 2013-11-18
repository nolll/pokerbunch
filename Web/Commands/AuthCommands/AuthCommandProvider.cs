using Core.Repositories;
using Core.Services;
using Infrastructure.System;

namespace Web.Commands.AuthCommands
{
    public class AuthCommandProvider : IAuthCommandProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IWebContext _webContext;

        public AuthCommandProvider(
            IUserRepository userRepository,
            IEncryptionService encryptionService,
            IWebContext webContext)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _webContext = webContext;
        }

        public Command GetLoginCommand(string loginName, string password, bool rememberMe)
        {
            return new LoginCommand(_userRepository, _encryptionService, _webContext, loginName, password, rememberMe);
        }
    }
}