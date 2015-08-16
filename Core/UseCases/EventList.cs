using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class EventList
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public EventList(IBunchRepository bunchRepository, IEventRepository eventRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var events = _eventRepository.Find(bunch.Id);

            var eventItems = events.OrderByDescending(o => o.StartDate).Select(o => CreateEventItem(request.Slug, o)).ToList();

            return new Result(eventItems);
        }

        private static Item CreateEventItem(string slug, Event e)
        {
            var eventDetailsUrl = new EventDetailsUrl(slug, e.Id);

            return new Item(e.Name, eventDetailsUrl, e.Location, e.StartDate, e.EndDate);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
            }
        }

        public class Result
        {
            public IList<Item> Events { get; private set; }

            public Result(IList<Item> events)
            {
                Events = events;
            }
        }

        public class Item
        {
            public string Name { get; private set; }
            public Url EventDetailsUrl { get; private set; }
            public string Location { get; private set; }
            public Date StartDate { get; private set; }
            public Date EndDate { get; private set; }

            public Item(string name, Url eventDetailsUrl, string location, Date startDate, Date endDate)
            {
                Name = name;
                EventDetailsUrl = eventDetailsUrl;
                Location = location;
                StartDate = startDate;
                EndDate = endDate;
            }
        }
    }
}