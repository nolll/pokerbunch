using Application.Services;

namespace Application.UseCases.AddBunchForm
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