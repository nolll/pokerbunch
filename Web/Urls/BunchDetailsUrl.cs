using Web.Common.Routes;

namespace Web.Urls
{
    public class BunchDetailsUrl : SlugUrl
    {
        public BunchDetailsUrl(string slug)
            : base(WebRoutes.BunchDetails, slug)
        {
        }
    }
}