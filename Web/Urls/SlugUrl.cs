namespace Web.Urls
{
    public abstract class SlugUrl : SiteUrl
    {
        protected SlugUrl(string format, string slug)
            : base(RouteParams.ReplaceSlug(format, slug))
        {
        }
    }
}