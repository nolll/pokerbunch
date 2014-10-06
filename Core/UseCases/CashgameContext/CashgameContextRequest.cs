namespace Core.UseCases.CashgameContext
{
    public class CashgameContextRequest
    {
        public string Slug { get; private set; }
        public int? Year { get; private set; }
        public CashgamePage SelectedPage { get; private set; }

        public CashgameContextRequest(string slug, int? year = null, CashgamePage selectedPage = CashgamePage.Unknown)
        {
            SelectedPage = selectedPage;
            Slug = slug;
            Year = year;
        }
    }
}