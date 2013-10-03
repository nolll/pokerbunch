using Core.Classes;
using Core.Services;
using Web.Formatters;
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

        public string GetAddUserUrl()
        {
            return RouteFormats.UserAdd;
        }

        public string GetAuthLoginUrl()
        {
            return RouteFormats.AuthLogin;
        }

        public string GetJoinHomegameUrl(Homegame homegame)
        {
            return UrlFormatter.FormatHomegame(RouteFormats.HomegameJoin, homegame);
        }

        public string GetTwitterCallbackUrl()
        {
            return string.Format(RouteFormats.TwitterCallback, _settings.GetSiteUrl());
        }
    }
}