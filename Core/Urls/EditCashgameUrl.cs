namespace Core.Urls
{
    public class EditCashgameUrl : CashgameUrl
    {
        public EditCashgameUrl(string slug, string dateStr)
            : base(Routes.CashgameEdit, slug, dateStr)
        {
        }
    }
}