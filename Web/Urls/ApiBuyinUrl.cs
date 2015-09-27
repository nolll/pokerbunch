using Web.Common.Routes;

namespace Web.Urls
{
    public class ApiBuyinUrl : ApiUrl
    {
        public ApiBuyinUrl(string slug)
            : base(ApiRoutes.Buyin)
        {
        }
    }
}