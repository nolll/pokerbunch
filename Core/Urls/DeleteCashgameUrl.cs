namespace Core.Urls
{
    public class DeleteCashgameUrl : Url
    {
        public DeleteCashgameUrl(int id)
            : base(RouteParams.ReplaceId(Routes.CashgameDelete, id))
        {
        }
    }
}