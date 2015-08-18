namespace Core.Urls
{
    public class AddCashgameUrl : SlugUrl
    {
        public AddCashgameUrl(string slug)
            : base(Routes.CashgameAdd, slug)
        {
        }
    }
}