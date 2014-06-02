namespace Web.Models.UrlModels
{
    public abstract class UrlModel
    {
        private readonly string _url;

        protected UrlModel(string url)
        {
            _url = url ?? string.Empty;
        }

        public override string ToString()
        {
            return _url;
        }
    }
}