using System.Collections.Generic;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases
{
    public class EditCashgameForm
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public EditCashgameForm(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            
            var cancelUrl = new CashgameDetailsUrl(bunch.Slug, cashgame.DateString);
            var deleteUrl = new DeleteCashgameUrl(bunch.Slug, cashgame.DateString);
            var location = cashgame.Location;
            var locations = _cashgameRepository.GetLocations(bunch.Id);

            return new Result(cashgame.DateString, cancelUrl, deleteUrl, location, locations);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public string DateStr { get; private set; }

            public Request(string slug, string dateStr)
            {
                Slug = slug;
                DateStr = dateStr;
            }
        }

        public class Result
        {
            public string Date { get; private set; }
            public Url CancelUrl { get; private set; }
            public Url DeleteUrl { get; private set; }
            public string Location { get; private set; }
            public IList<string> Locations { get; private set; }

            public Result(string date, Url cancelUrl, Url deleteUrl, string location, IList<string> locations)
            {
                Date = date;
                CancelUrl = cancelUrl;
                DeleteUrl = deleteUrl;
                Location = location;
                Locations = locations;
            }
        }
    }
}