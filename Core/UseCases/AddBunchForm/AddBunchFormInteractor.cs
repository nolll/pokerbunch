using Core.Services;

namespace Core.UseCases.AddBunchForm
{
    public class AddBunchFormInteractor
    {
        public AddBunchFormResult Execute()
        {
            var timeZones = TimeZoneService.GetTimeZones();
            var currencyLayouts = Globalization.GetCurrencyLayouts();

            return new AddBunchFormResult(timeZones, currencyLayouts);
        }
    }
}