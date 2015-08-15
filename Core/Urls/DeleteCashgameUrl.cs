namespace Core.Urls
{
    public class DeleteCashgameUrl : CashgameUrl
    {
        public DeleteCashgameUrl(string slug, string dateStr)
            : base(Routes.CashgameDelete, slug, dateStr)
        {
        }
    }
}