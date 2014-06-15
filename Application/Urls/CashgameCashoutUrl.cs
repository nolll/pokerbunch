namespace Application.Urls
{
    public class CashgameCashoutUrl : PlayerUrl
    {
        public CashgameCashoutUrl(string slug, int playerId)
            : base(RouteFormats.CashgameCashout, slug, playerId)
        {
        }
    }
}