using System.Collections.Generic;
using Core.Urls;

namespace Core.UseCases.EditBunchForm
{
    public class EditBunchFormResult
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

        public EditBunchFormResult(string heading, Url cancelUrl, string description, string houseRules, int defaultBuyin, string timeZoneId, string currencySymbol, string currencyLayout, IList<AddBunchForm.TimeZoneItem> timeZones, IList<string> currencyLayouts)
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