namespace Application.Urls
{
    public class JoinHomeGameUrl : HomegameUrl
    {
        public JoinHomeGameUrl(string slug)
            : base(RouteFormats.HomegameJoin, slug)
        {
        }
    }
}