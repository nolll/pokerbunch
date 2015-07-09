namespace Core.UseCases.BunchContext
{
    public class BunchContextRequest
    {
        public string UserName { get; private set; }
        public string Slug { get; private set; }

        public BunchContextRequest(string userName, string slug = null)
        {
            UserName = userName;
            Slug = slug;
        }

        public bool HasSlug
        {
            get { return !string.IsNullOrEmpty(Slug); }
        }
    }
}