using Core.Exceptions;
using Core.Repositories;
using Core.UseCases.BaseContext;

namespace Core.UseCases.AppContext
{
    public class AppContextInteractor
    {
        private readonly IUserRepository _userRepository;

        public AppContextInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AppContextResult Execute(AppContextRequest request)
        {
            var isAuthenticated = !string.IsNullOrEmpty(request.UserName);
            var userName = isAuthenticated ? request.UserName : string.Empty;
            var user = isAuthenticated ? _userRepository.GetByNameOrEmail(userName) : null;
            if (isAuthenticated && user == null) // Broken auth cookie
                throw new NotLoggedInException();
            var userId = isAuthenticated ? user.Id : 0;
            var userDisplayName = isAuthenticated ? user.DisplayName : string.Empty;
            var isAdmin = isAuthenticated && user.IsAdmin;
            var baseContextResult = new BaseContextInteractor().Execute();

            return new AppContextResult(
                baseContextResult,
                isAuthenticated,
                isAdmin,
                userId,
                userName,
                userDisplayName);
        }
    }
}