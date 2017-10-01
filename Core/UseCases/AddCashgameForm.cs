using System.Collections.Generic;
using System.Linq;
using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class AddCashgameForm
    {
        private readonly IBunchService _bunchService;
        private readonly ICashgameService _cashgameService;
        private readonly ILocationService _locationService;

        public AddCashgameForm(IBunchService bunchService, ICashgameService cashgameService, ILocationService locationService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _locationService = locationService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.Slug);
            var runningGame = _cashgameService.GetCurrent(bunch.Id);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _locationService.List(bunch.Id);
            var locationItems = locations.Select(o => new LocationItem(o.Id, o.Name)).ToList();
            return new Result(locationItems);
        }

        public class Request
        {
            public string Slug { get; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }

        public class Result
        {
            public IList<LocationItem> Locations { get; }

            public Result(IList<LocationItem> locations)
            {
                Locations = locations;
            }
        }

        public class LocationItem
        {
            public string Id { get; }
            public string Name { get; }

            public LocationItem(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public class EventItem
        {
            public string Id { get; }
            public string Name { get; }

            public EventItem(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}