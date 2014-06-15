namespace Application.Urls
{
    public class EndCashgameUrl : HomegameUrl
    {
        public EndCashgameUrl(string slug)
            : base(RouteFormats.CashgameEnd, slug)
        {
        }
    }
}