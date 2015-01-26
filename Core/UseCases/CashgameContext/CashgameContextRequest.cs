using Core.UseCases.BunchContext;

namespace Core.UseCases.CashgameContext
{
    public class CashgameContextRequest : BunchContextRequest
    {
        public int? Year { get; private set; }
        public CashgamePage SelectedPage { get; private set; }

        public CashgameContextRequest(string slug, int? year = null, CashgamePage selectedPage = CashgamePage.Unknown)
            : base(slug)
        {
            Year = year;
            SelectedPage = selectedPage;
        }
    }
}