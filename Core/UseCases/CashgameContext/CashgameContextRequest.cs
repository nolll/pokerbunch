using Core.UseCases.BunchContext;

namespace Core.UseCases.CashgameContext
{
    public class CashgameContextRequest : BunchContextRequest
    {
        public CashgamePage SelectedPage { get; private set; }
        public int? Year { get; private set; }

        public CashgameContextRequest(string slug, CashgamePage selectedPage = CashgamePage.Unknown, int? year = null)
            : base(slug)
        {
            SelectedPage = selectedPage;
            Year = year;
        }
    }
}