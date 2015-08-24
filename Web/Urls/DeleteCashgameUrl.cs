using Core.Urls;

namespace Web.Urls
{
    public class DeleteCashgameUrl : IdUrl
    {
        public DeleteCashgameUrl(int id)
            : base(Routes.CashgameDelete, id)
        {
        }
    }
}