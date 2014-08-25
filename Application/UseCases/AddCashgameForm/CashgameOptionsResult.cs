using System.Collections.Generic;

namespace Application.UseCases.AddCashgameForm
{
    public class CashgameOptionsResult
    {
        public IList<string> Locations { get; private set; }

        public CashgameOptionsResult(IList<string> locations)
        {
            Locations = locations;
        }
    }
}