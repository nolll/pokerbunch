namespace Core.Urls
{
    public abstract class IdUrl : Url
    {
        protected IdUrl(string format, int id)
            : base(RouteParams.ReplaceId(format, id))
        {
        }
    }
}