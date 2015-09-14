namespace Web.Common.Urls
{
    public abstract class Url
    {
        private readonly string _url;

        protected Url(string url)
        {
            _url = url != null ? string.Concat("/", url) : string.Empty;
        }

        public string Relative
        {
            get { return _url; }
        }

        public string Absolute
        {
            get { return string.Format("http://{0}{1}", GetDomainName(), _url); }
        }

        public abstract string GetDomainName();

        public override string ToString()
        {
            return Relative;
        }
    }
}