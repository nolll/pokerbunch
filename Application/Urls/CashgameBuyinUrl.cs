namespace Application.Urls
{
    public class CashgameBuyinUrl : PlayerUrl
    {
        public CashgameBuyinUrl(string slug, int playerId)
            : base(RouteFormats.CashgameBuyin, slug, playerId)
        {
        }
    }
}