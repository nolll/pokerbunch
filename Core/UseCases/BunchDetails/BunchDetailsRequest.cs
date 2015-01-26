namespace Core.UseCases.BunchDetails
{
    public class BunchDetailsRequest
    {
        public string Slug { get; private set; }

        public BunchDetailsRequest(string slug)
        {
            Slug = slug;
        }
    }
}