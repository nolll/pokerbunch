using System.Collections.Generic;

namespace Core.UseCases.AddCashgameForm
{
    public class AddCashgameFormResult
    {
        public IList<string> Locations { get; private set; }

        public AddCashgameFormResult(IList<string> locations)
        {
            Locations = locations;
        }
    }
}