namespace Application.UseCases.CashgameOptions
{
    public class CashgameOptionsRequest
    {
        public string Slug { private set; get; }

        public CashgameOptionsRequest(string slug)
        {
            Slug = slug;
        }
    }
}