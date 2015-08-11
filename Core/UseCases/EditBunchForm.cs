using System.Collections.Generic;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class EditBunchForm
    {
        private readonly IBunchRepository _bunchRepository;

        public EditBunchForm(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public Result Execute(Request request)
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
            
            return new Result(heading, cancelUrl, description, houseRules, defaultBuyin, timeZoneId, currencySymbol, currencyLayout, timeZones, currencyLayouts);
        }

        public class Request
        {
            public string Slug { get; private set; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }

        public class Result
        {
            public string Heading { get; private set; }
            public Url CancelUrl { get; private set; }
            public string Description { get; private set; }
            public string HouseRules { get; private set; }
            public int DefaultBuyin { get; private set; }
            public string TimeZoneId { get; private set; }
            public string CurrencySymbol { get; private set; }
            public string CurrencyLayout { get; private set; }
            public IList<AddBunchForm.TimeZoneItem> TimeZones { get; private set; }
            public IList<string> CurrencyLayouts { get; private set; }

            public Result(string heading, Url cancelUrl, string description, string houseRules, int defaultBuyin, string timeZoneId, string currencySymbol, string currencyLayout, IList<AddBunchForm.TimeZoneItem> timeZones, IList<string> currencyLayouts)
            {
                Heading = heading;
                CancelUrl = cancelUrl;
                Description = description;
                HouseRules = houseRules;
                DefaultBuyin = defaultBuyin;
                TimeZoneId = timeZoneId;
                CurrencySymbol = currencySymbol;
                CurrencyLayout = currencyLayout;
                TimeZones = timeZones;
                CurrencyLayouts = currencyLayouts;
            }
        }
    }
}