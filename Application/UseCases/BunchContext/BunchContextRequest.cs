namespace Application.UseCases.BunchContext
{
    public class BunchContextRequest
    {
        public string Slug { get; private set; }

        public BunchContextRequest(string slug = null)
        {
            Slug = slug;
        }

        public bool HasSlug
        {
            get { return !string.IsNullOrEmpty(Slug); }
        }
    }
}