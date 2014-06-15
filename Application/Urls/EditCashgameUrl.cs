namespace Application.Urls
{
    public class EditCashgameUrl : CashgameUrl
    {
        public EditCashgameUrl(string slug, string dateStr)
            : base(RouteFormats.CashgameEdit, slug, dateStr)
        {
        }
    }
}