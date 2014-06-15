using Application.Services;
using Application.Urls;
using Web.Models.UrlModels;

namespace Web.Services
{
    public class UrlProvider : IUrlProvider
    {
        private readonly ISettings _settings;

        public UrlProvider(ISettings settings)
        {
            _settings = settings;
        }

        /* leave for now */
        public string GetLoginUrl()
        {
            return new LoginUrl().Relative;
        }

        /* leave for now */
        public string GetAddUserUrl()
        {
            return new AddUserUrl().Relative;
        }

        /* leave for now */
        public string GetJoinHomegameUrl(string slug)
        {
            return new JoinHomeGameUrl(slug).Relative;
        }

        /* leave for now */
        public string GetTwitterCallbackUrl()
        {
            return new TwitterCallbackUrl().Absolute();
        }
    }
}