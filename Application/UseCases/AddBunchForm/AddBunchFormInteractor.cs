using Application.Services;

namespace Application.UseCases.AddBunchForm
{
    public class AddBunchFormInteractor : IAddBunchFormInteractor
    {
        public AddBunchFormResult Execute()
        {
            var timeZones = TimeZoneService.GetTimeZones();
            var currencyLayouts = Globalization.GetCurrencyLayouts();

            return new AddBunchFormResult(timeZones, currencyLayouts);
        }
    }
}