namespace Application.UseCases.BunchContext
{
    public class BunchContextRequest
    {
        public string Slug { get; set; }

        public bool HasSlug
        {
            get { return !string.IsNullOrEmpty(Slug); }
        }
    }
}