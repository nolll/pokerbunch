namespace Infrastructure.ApiUrls
{
    public class SimpleApiUrl : ApiUrl
    {
        public string Url { get; }

        public SimpleApiUrl(string url)
        {
            Url = url;
        }
    }
}