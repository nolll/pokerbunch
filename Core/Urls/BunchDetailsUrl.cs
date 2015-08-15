namespace Core.Urls
{
    public class BunchDetailsUrl : BunchUrl
    {
        public BunchDetailsUrl(string slug)
            : base(Routes.BunchDetails, slug)
        {
        }
    }
}