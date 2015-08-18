namespace Core.Urls
{
    public class DeleteCashgameUrl : IdUrl
    {
        public DeleteCashgameUrl(int id)
            : base(Routes.CashgameDelete, id)
        {
        }
    }
}