namespace Web.Common.Urls
{
    public abstract class Url
    {
        protected Url(string url)
        {
            Relative = url != null ? string.Concat("/", url) : string.Empty;
        }

        public string Relative { get; }

        public string Absolute => $"https://{GetDomainName()}{Relative}";

        protected abstract string GetDomainName();

        public override string ToString()
        {
            return Relative;
        }
    }
}