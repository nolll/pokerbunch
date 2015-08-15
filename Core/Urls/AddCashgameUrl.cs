namespace Core.Urls
{
    public class AddCashgameUrl : BunchUrl
    {
        public AddCashgameUrl(string slug)
            : base(Routes.CashgameAdd, slug)
        {
        }
    }
}