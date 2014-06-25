namespace Application.UseCases.AddCashgameForm
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