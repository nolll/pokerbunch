using Application.Services;
using Web.Models.UrlModels;
using Web.Routing;

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
            return new LoginUrlModel().Relative;
        }

        /* leave for now */
        public string GetAddUserUrl()
        {
            return new AddUserUrlModel().Relative;
        }

        /* leave for now */
        public string GetJoinHomegameUrl(string slug)
        {
            return new JoinHomeGameUrlModel(slug).Relative;
        }

        /* leave for now */
        public string GetTwitterCallbackUrl()
        {
            return new TwitterCallbackUrlModel().Absolute;
        }
    }
}