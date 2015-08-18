namespace Core.Urls
{
    public class BunchDetailsUrl : SlugUrl
    {
        public BunchDetailsUrl(string slug)
            : base(Routes.BunchDetails, slug)
        {
        }
    }
}