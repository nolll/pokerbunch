namespace Core.UseCases.DeleteCashgame
{
    public class DeleteCashgameRequest
    {
        public string Slug { get; private set; }
        public int CashgameId { get; private set; }

        public DeleteCashgameRequest(string slug, int cashgameId)
        {
            Slug = slug;
            CashgameId = cashgameId;
        }
    }
}