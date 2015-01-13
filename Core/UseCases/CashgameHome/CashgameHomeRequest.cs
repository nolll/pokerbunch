namespace Core.UseCases.CashgameHome
{
    public class CashgameHomeRequest
    {
        public string Slug { get; private set; }

        public CashgameHomeRequest(string slug)
        {
            Slug = slug;
        }
    }
}