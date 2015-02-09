using System.Collections.Generic;
using Core.Urls;

namespace Core.UseCases.EditCashgameForm
{
    public class EditCashgameFormResult
    {
        public string Date { get; private set; }
        public Url CancelUrl { get; private set; }
        public Url DeleteUrl { get; private set; }
        public string Location { get; private set; }
        public IList<string> Locations { get; private set; }

        public EditCashgameFormResult(string date, Url cancelUrl, Url deleteUrl, string location, IList<string> locations)
        {
            Date = date;
            CancelUrl = cancelUrl;
            DeleteUrl = deleteUrl;
            Location = location;
            Locations = locations;
        }
    }
}