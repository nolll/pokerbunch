using Core.Repositories;

namespace Core.Services
{
    public class AuthService
    {
        private readonly ITokenRepository _tokenRepository;

        public AuthService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public string GetToken(string userName, string password)
        {
            return _tokenRepository.Get(userName, password);
        }
    }
}