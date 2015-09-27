using Web.Common.Routes;

namespace Web.Urls
{
    public class EditCashgameUrl : IdUrl
    {
        public EditCashgameUrl(int id)
            : base(WebRoutes.CashgameEdit, id)
        {
        }
    }
}