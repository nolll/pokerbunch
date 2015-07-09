using System;
using Core.UseCases.BunchContext;

namespace Core.UseCases.CashgameContext
{
    public class CashgameContextRequest : BunchContextRequest
    {
        public DateTime CurrentTime { get; private set; }
        public CashgamePage SelectedPage { get; private set; }
        public int? Year { get; private set; }

        public CashgameContextRequest(string userName, string slug, DateTime currentTime, CashgamePage selectedPage = CashgamePage.Unknown, int? year = null)
            : base(userName, slug)
        {
            CurrentTime = currentTime;
            SelectedPage = selectedPage;
            Year = year;
        }
    }
}