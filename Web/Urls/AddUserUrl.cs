using Core.Urls;

namespace Web.Urls
{
    public class AddUserUrl : Url
    {
        public AddUserUrl()
            : base(RouteFormats.UserAdd)
        {
        }
    }
}