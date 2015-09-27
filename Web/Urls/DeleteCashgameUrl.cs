using Web.Common.Routes;

namespace Web.Urls
{
    public class DeleteCashgameUrl : IdUrl
    {
        public DeleteCashgameUrl(int id)
            : base(WebRoutes.CashgameDelete, id)
        {
        }
    }
}