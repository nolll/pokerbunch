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

    public class AppContextRequest
    {
        public string UserName { get; private set; }

        public AppContextRequest(string userName)
        {
            UserName = userName;
        }
    }
}