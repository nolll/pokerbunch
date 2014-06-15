namespace Application.Urls
{
    public class CashgameIndexUrl : HomegameUrl
    {
        public CashgameIndexUrl(string slug)
            : base(RouteFormats.CashgameIndex, slug)
        {
        }
    }
}