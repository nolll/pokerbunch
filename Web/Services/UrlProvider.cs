using Core.Classes;
using Core.Services;
using Web.Formatters;
using Web.Routing;

namespace Web.Services
{
    public class UrlProvider : IUrlProvider
    {
        public string GetJoinHomegameUrl(Homegame homegame)
        {
            return UrlFormatter.FormatHomegame(RouteFormats.HomegameJoin, homegame);
        }

        public string GetAddUserUrl()
        {
            return RouteFormats.UserAdd;
        }
    }
}