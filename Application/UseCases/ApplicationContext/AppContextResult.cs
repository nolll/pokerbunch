namespace Application.UseCases.ApplicationContext
{
    public class AppContextResult
    {
        public bool IsLoggedIn { get; private set; }
        public bool IsAdmin { get; private set; }
        public string UserName { get; private set; }
        public string UserDisplayName { get; private set; }
        public bool IsInProduction { get; private set; }
        public string Version { get; private set; }

        public AppContextResult(
            bool isLoggedIn,
            bool isAdmin,
            string userName,
            string userDisplayName,
            bool isInProduction,
            string version)
        {
            IsLoggedIn = isLoggedIn;
            IsAdmin = isAdmin;
            UserName = userName;
            UserDisplayName = userDisplayName;
            IsInProduction = isInProduction;
            Version = version;
        }
    }
}