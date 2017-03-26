using System;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class AddBunch
    {
        private readonly IBunchService _bunchService;

        public AddBunch(IBunchService bunchService)
        {
            _bunchService = bunchService;
        }

        public void Execute(Request request)
        {
            var bunch = CreateBunch(request);
            _bunchService.Add(bunch);
        }

        private static Bunch CreateBunch(Request request)
        {
            var slug = SlugGenerator.GetSlug(request.DisplayName);
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone);
            var currency = new Currency(request.CurrencySymbol, request.CurrencyLayout);

            return new Bunch(slug, request.DisplayName, request.Description, string.Empty, timezone, 200, currency);
        }

        public class Request
        {
            public string DisplayName { get; }
            public string Description { get; }
            public string CurrencySymbol { get; }
            public string CurrencyLayout { get; }
            public string TimeZone { get; }

            public Request(string displayName, string description, string currencySymbol, string currencyLayout, string timeZone)
            {
                DisplayName = displayName;
                Description = description;
                CurrencySymbol = currencySymbol;
                CurrencyLayout = currencyLayout;
                TimeZone = timeZone;
            }
        }
    }
}
