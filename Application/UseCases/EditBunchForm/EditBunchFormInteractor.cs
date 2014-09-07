using Application.Services;
using Application.Urls;
using Core.Repositories;

namespace Application.UseCases.EditBunchForm
{
    public static class EditBunchFormInteractor
    {
        public static EditBunchFormResult Execute(IBunchRepository bunchRepository, EditBunchFormRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var heading = string.Format("{0} Settings", bunch.DisplayName);
            var cancelUrl = new BunchDetailsUrl(bunch.Slug);
            var description = bunch.Description;
            var houseRules = bunch.HouseRules;
            var defaultBuyin = bunch.DefaultBuyin;
            var timeZoneId = bunch.Timezone.Id;
            var currencySymbol = bunch.Currency.Symbol;
            var currencyLayout = bunch.Currency.Layout;
            var timeZones = TimeZoneService.GetTimeZones();
            var currencyLayouts = Globalization.GetCurrencyLayouts();
            
            return new EditBunchFormResult(heading, cancelUrl, description, houseRules, defaultBuyin, timeZoneId, currencySymbol, currencyLayout, timeZones, currencyLayouts);
        }
    }
}