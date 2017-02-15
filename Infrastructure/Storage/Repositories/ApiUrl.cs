namespace Infrastructure.Storage.Repositories
{
    public class ApiUrl
    {
        public string Url { get; }

        public ApiUrl(string url)
        {
            Url = url;
        }
    }
}