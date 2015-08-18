namespace Core.Urls
{
    public abstract class SlugUrl : Url
    {
        protected SlugUrl(string format, string slug)
            : base(RouteParams.ReplaceSlug(format, slug))
        {
        }
    }
}