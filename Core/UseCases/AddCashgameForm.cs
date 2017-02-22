﻿using System.Collections.Generic;
using System.Linq;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class AddCashgameForm
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IEventRepository _eventRepository;

        public AddCashgameForm(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, ILocationRepository locationRepository, IEventRepository eventRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _locationRepository = locationRepository;
            _eventRepository = eventRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.Slug);
            var runningGame = _cashgameRepository.GetCurrent(bunch.Id);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _locationRepository.List(bunch.Id);
            var locationItems = locations.Select(o => new LocationItem(o.Id, o.Name)).ToList();
            var events = _eventRepository.ListByBunch(bunch.Id);
            var eventItems = events.Select(o => new EventItem(o.Id, o.Name)).ToList();
            return new Result(locationItems, eventItems);
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
            public IList<LocationItem> Locations { get; private set; }
            public IList<EventItem> Events { get; private set; }

            public Result(IList<LocationItem> locations, IList<EventItem> events)
            {
                Locations = locations;
                Events = events;
            }
        }

        public class LocationItem
        {
            public string Id { get; private set; }
            public string Name { get; private set; }

            public LocationItem(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public class EventItem
        {
            public string Id { get; private set; }
            public string Name { get; private set; }

            public EventItem(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}