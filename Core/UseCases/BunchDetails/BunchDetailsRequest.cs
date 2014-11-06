namespace Core.UseCases.BunchDetails
{
    public class BunchDetailsRequest
    {
        public string Slug { get; private set; }
        public string UserName { get; private set; }

        public BunchDetailsRequest(string slug, string userName)
        {
            Slug = slug;
            UserName = userName;
        }
    }
}