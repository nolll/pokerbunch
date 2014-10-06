namespace Core.Urls
{
    public abstract class BunchUrl : Url
    {
        protected BunchUrl(string format, string slug)
            : base(string.Format(format, slug))
        {
        }
    }
}