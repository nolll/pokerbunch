namespace Core.Urls
{
    public class JoinBunchUrl : SlugUrl
    {
        public JoinBunchUrl(string slug)
            : base(Routes.BunchJoin, slug)
        {
        }
    }
}