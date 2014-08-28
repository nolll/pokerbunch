using System.Collections.Generic;

namespace Application.UseCases.AddBunchForm
{
    public class AddBunchFormResult
    {
        public IList<TimeZoneItem> TimeZones { get; private set; }
        public IList<string> CurrencyLayouts { get; private set; }

        public AddBunchFormResult(IList<TimeZoneItem> timeZones, IList<string> currencyLayouts)
        {
            TimeZones = timeZones;
            CurrencyLayouts = currencyLayouts;
        }
    }
}