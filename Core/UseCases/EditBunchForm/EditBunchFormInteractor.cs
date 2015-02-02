using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.EditBunchForm
{
    public class EditBunchFormInteractor
    {
        private readonly IBunchRepository _bunchRepository;

        public EditBunchFormInteractor(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public EditBunchFormResult Execute(EditBunchFormRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
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