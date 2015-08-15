namespace Core.Urls
{
    public abstract class BunchUrl : Url
    {
        protected BunchUrl(string format, string slug)
            : base(RouteParams.ReplaceSlug(format, slug))
        {
        }
    }
}