namespace Core.UseCases.CashgameStatus
{
    public class CashgameStatusRequest
    {
        public string Slug { get; private set; }

        public CashgameStatusRequest(string slug)
        {
            Slug = slug;
        }
    }
}