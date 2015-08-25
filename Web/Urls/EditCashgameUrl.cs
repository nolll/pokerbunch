using Core.Urls;

namespace Web.Urls
{
    public class EditCashgameUrl : IdUrl
    {
        public EditCashgameUrl(int id)
            : base(Routes.CashgameEdit, id)
        {
        }
    }
}