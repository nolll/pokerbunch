namespace Web.Urls
{
    public abstract class IdUrl : SiteUrl
    {
        protected IdUrl(string format, int id)
            : base(RouteParams.ReplaceId(format, id))
        {
        }
    }
}