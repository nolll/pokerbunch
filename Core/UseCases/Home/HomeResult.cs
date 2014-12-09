using Core.Urls;

namespace Core.UseCases.Home
{
    public class HomeResult
    {
        public bool IsLoggedIn { get; private set; }
        public bool IsAdmin { get; private set; }
        public Url AddBunchUrl { get; private set; }
        public Url LoginUrl { get; private set; }
        public Url AddUserUrl { get; private set; }
        public Url UserListUrl { get; private set; }
        public Url BunchListUrl { get; private set; }
        public Url TestEmailUrl { get; private set; }
        public Url ClearCacheUrl { get; private set; }

        public HomeResult(bool isLoggedIn, bool isAdmin)
        {
            IsLoggedIn = isLoggedIn;
            IsAdmin = isAdmin;
            AddBunchUrl = new AddBunchUrl();
            LoginUrl = new LoginUrl();
            AddUserUrl = new AddUserUrl();
            UserListUrl = new UserListUrl();
            BunchListUrl = new BunchListUrl();
            TestEmailUrl = new TestEmailUrl();
            ClearCacheUrl = new ClearCacheUrl();
        }
    }
}