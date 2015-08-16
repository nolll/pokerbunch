namespace Core.Urls
{
    public class CashgameDetailsUrl : Url
    {
        public CashgameDetailsUrl(int id)
            : base(RouteParams.ReplaceId(Routes.CashgameDetails, id))
        {
        }
    }
}