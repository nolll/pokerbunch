namespace Core.UseCases.JoinBunchForm
{
    public class JoinBunchFormRequest
    {
        public string Slug { get; private set; }

        public JoinBunchFormRequest(string slug)
        {
            Slug = slug;
        }
    }
}