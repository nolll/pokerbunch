namespace Application.Urls
{
    public class JoinBunchUrl : BunchUrl
    {
        public JoinBunchUrl(string slug)
            : base(RouteFormats.BunchJoin, slug)
        {
        }
    }
}