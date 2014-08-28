using System.Linq;
using Application.Services;

namespace Application.UseCases.AddBunchForm
{
    public class AddBunchFormInteractor : IAddBunchFormInteractor
    {
        public AddBunchFormResult Execute()
        {
            var timeZones = Globalization.GetTimezones();
            var timeZoneItems = timeZones.Select(t => new TimeZoneItem(t.Id, t.DisplayName)).ToList();

            var currencyLayouts = Globalization.GetCurrencyLayouts();

            return new AddBunchFormResult(timeZoneItems, currencyLayouts);
        }
    }
}