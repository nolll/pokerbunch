using Application.Urls;

namespace Application.UseCases.Home
{
    public class HomeResult
    {
        public Url AddBunchUrl { get; private set; }
        public Url LoginUrl { get; private set; }
        public Url AddUserUrl { get; private set; }
        public bool IsLoggedIn { get; private set; }
        public bool IsAdmin { get; private set; }

        public HomeResult(bool isLoggedIn, bool isAdmin)
        {
            IsLoggedIn = isLoggedIn;
            IsAdmin = isAdmin;
            AddBunchUrl = new AddBunchUrl();
            LoginUrl = new LoginUrl();
            AddUserUrl = new AddUserUrl();
        }
    }
}