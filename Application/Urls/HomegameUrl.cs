namespace Application.Urls
{
    public abstract class HomegameUrl : Url
    {
        protected HomegameUrl(string format, string slug)
            : base(string.Format(format, slug))
        {
        }
    }
}