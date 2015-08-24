using Core.Urls;

namespace Web.Urls
{
    public class CashgameDetailsUrl : IdUrl
    {
        public CashgameDetailsUrl(int id)
            : base(Routes.CashgameDetails, id)
        {
        }
    }
}