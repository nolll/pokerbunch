namespace Application.UseCases.AppContext
{
    public class AppContextResult : BaseContextResult
    {
        public bool IsLoggedIn { get; private set; }
        public bool IsAdmin { get; private set; }
        public string UserName { get; private set; }
        public string UserDisplayName { get; private set; }

        public AppContextResult(
            BaseContextResult baseContextResult,
            bool isLoggedIn,
            bool isAdmin,
            string userName,
            string userDisplayName)

            : base(
            baseContextResult.IsInProduction,
            baseContextResult.Version)
        {
            IsLoggedIn = isLoggedIn;
            IsAdmin = isAdmin;
            UserName = userName;
            UserDisplayName = userDisplayName;
        }
    }

    public class BaseContextResult
    {
        public bool IsInProduction { get; private set; }
        public string Version { get; private set; }

        public BaseContextResult(
            bool isInProduction,
            string version)
        {
            IsInProduction = isInProduction;
            Version = version;
        }
    }
}