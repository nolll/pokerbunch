namespace Application.UseCases.CashgameContext
{
    public class ApplicationContextResult
    {
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public string UserName { get; set; }
        public string UserDisplayName { get; set; }
        public bool IsInProduction { get; set; }
        public string Version { get; set; }

        public ApplicationContextResult(
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

    public class ApplicationContextResultInTest : ApplicationContextResult
    {
        public ApplicationContextResultInTest(
            bool isLoggedIn = false, 
            bool isAdmin = false, 
            string userName = null, 
            string userDisplayName = null, 
            bool isInProduction = false, 
            string version = null)
            
            : base(
            isLoggedIn, 
            isAdmin, 
            userName, 
            userDisplayName, 
            isInProduction, 
            version)
        {
        }
    }
}