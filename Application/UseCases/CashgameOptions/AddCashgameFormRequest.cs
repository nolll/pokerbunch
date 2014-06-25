namespace Application.UseCases.CashgameOptions
{
    public class AddCashgameFormRequest
    {
        public string Slug { private set; get; }

        public AddCashgameFormRequest(string slug)
        {
            Slug = slug;
        }
    }
}