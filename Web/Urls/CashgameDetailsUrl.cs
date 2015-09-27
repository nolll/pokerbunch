using Web.Common.Routes;

namespace Web.Urls
{
    public class CashgameDetailsUrl : IdUrl
    {
        public CashgameDetailsUrl(int id)
            : base(WebRoutes.CashgameDetails, id)
        {
        }
    }
}