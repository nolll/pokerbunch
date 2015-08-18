namespace Core.Urls
{
    public class EditCashgameUrl : IdUrl
    {
        public EditCashgameUrl(int id)
            : base(Routes.CashgameEdit, id)
        {
        }
    }
}