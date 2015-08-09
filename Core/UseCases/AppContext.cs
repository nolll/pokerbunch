using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases
{
    public class AppContext
    {
        private readonly IUserRepository _userRepository;

        public AppContext(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var isAuthenticated = !string.IsNullOrEmpty(request.UserName);
            var userName = isAuthenticated ? request.UserName : string.Empty;
            var user = isAuthenticated ? _userRepository.GetByNameOrEmail(userName) : null;
            if (isAuthenticated && user == null) // Broken auth cookie
                throw new NotLoggedInException();
            var userId = isAuthenticated ? user.Id : 0;
            var userDisplayName = isAuthenticated ? user.DisplayName : string.Empty;
            var isAdmin = isAuthenticated && user.IsAdmin;
            var baseContextResult = new BaseContext().Execute();

            return new Result(
                baseContextResult,
                isAuthenticated,
                isAdmin,
                userId,
                userName,
                userDisplayName);
        }

        public class Request
        {
            public string UserName { get; private set; }

            public Request(string userName)
            {
                UserName = userName;
            }
        }

        public class Result
        {
            public bool IsLoggedIn { get; private set; }
            public bool IsAdmin { get; private set; }
            public int UserId { get; private set; }
            public string UserDisplayName { get; private set; }
            public BaseContext.Result BaseContext { get; private set; }
            public Url LoginUrl { get; private set; }
            public Url ForgotPasswordUrl { get; private set; }
            public Url LogoutUrl { get; private set; }
            public Url UserDetailsUrl { get; private set; }

            public Result(
                BaseContext.Result baseContextResult,
                bool isLoggedIn,
                bool isAdmin,
                int userId,
                string userName,
                string userDisplayName)
            {
                BaseContext = baseContextResult;
                IsLoggedIn = isLoggedIn;
                IsAdmin = isAdmin;
                UserId = userId;
                UserDisplayName = userDisplayName;
                LoginUrl = new LoginUrl();
                ForgotPasswordUrl = new ForgotPasswordUrl();
                LogoutUrl = new LogoutUrl();
                UserDetailsUrl = new UserDetailsUrl(userName);
            }
        }
    }
}