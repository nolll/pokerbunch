namespace Application.Urls
{
    public class HomegameDetailsUrl : HomegameUrl
    {
        public HomegameDetailsUrl(string slug)
            : base(RouteFormats.HomegameDetails, slug)
        {
        }
    }
}