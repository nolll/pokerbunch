using Application.Services.Interfaces;
using Core.Repositories;
using Infrastructure.System;
using Web.Models.AuthModels;

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

        public Command GetLoginCommand(AuthLoginPostModel postModel)
        {
            return new LoginCommand(_userRepository, _encryptionService, _webContext, postModel);
        }

        public Command GetLogoutCommand()
        {
            return new LogoutCommand(_webContext);
        }
    }
}