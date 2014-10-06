using Core.Services;

namespace Core.UseCases.AddBunchForm
{
    public static class AddBunchFormInteractor
    {
        public static AddBunchFormResult Execute()
        {
            var timeZones = TimeZoneService.GetTimeZones();
            var currencyLayouts = Globalization.GetCurrencyLayouts();

            return new AddBunchFormResult(timeZones, currencyLayouts);
        }
    }
}