namespace Application.Urls
{
    public class AddCashgameUrl : HomegameUrl
    {
        public AddCashgameUrl(string slug)
            : base(RouteFormats.CashgameAdd, slug)
        {
        }
    }
}