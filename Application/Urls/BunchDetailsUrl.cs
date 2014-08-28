namespace Application.Urls
{
    public class BunchDetailsUrl : BunchUrl
    {
        public BunchDetailsUrl(string slug)
            : base(RouteFormats.BunchDetails, slug)
        {
        }
    }
}