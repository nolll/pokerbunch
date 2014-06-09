namespace Web.Models.UrlModels
{
    public class UrlModel
    {
        public string Relative { get; private set; }

        public UrlModel(string url)
        {
            Relative = url ?? string.Empty;
        }
    }
}