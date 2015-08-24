using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class CashgameList
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public CashgameList(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var cashgames = _cashgameRepository.GetFinished(bunch.Id, request.Year);
            cashgames = SortItems(cashgames, request.SortOrder).ToList();
            var spansMultipleYears = CashgameService.SpansMultipleYears(cashgames);
            var list = cashgames.Select(o => new Item(bunch, o));

            return new Result(request.Slug, list.ToList(), request.SortOrder, request.Year, spansMultipleYears);
        }

        private static IEnumerable<Cashgame> SortItems(IEnumerable<Cashgame> items, SortOrder orderBy)
        {
            switch (orderBy)
            {
                case SortOrder.PlayerCount:
                    return items.OrderByDescending(o => o.PlayerCount);
                case SortOrder.Location:
                    return items.OrderByDescending(o => o.Location);
                case SortOrder.Duration:
                    return items.OrderByDescending(o => o.Duration);
                case SortOrder.Turnover:
                    return items.OrderByDescending(o => o.Turnover);
                case SortOrder.AverageBuyin:
                    return items.OrderByDescending(o => o.AverageBuyin);
                default:
                    return items.OrderByDescending(o => o.StartTime);
            }
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public SortOrder SortOrder { get; private set; }
            public int? Year { get; private set; }

            public Request(string userName, string slug, SortOrder sortOrder, int? year)
            {
                UserName = userName;
                Slug = slug;
                SortOrder = sortOrder;
                Year = year;
            }
        }

        public class Result
        {
            public IList<Item> List { get; private set; }
            public SortOrder SortOrder { get; private set; }
            public string Slug { get; private set; }
            public int? Year { get; private set; }
            public bool ShowYear { get; private set; }
            public bool SpansMultipleYears { get; private set; }

            public Result(string slug, IList<Item> list, SortOrder sortOrder, int? year, bool spansMultipleYears)
            {
                Slug = slug;
                List = list;
                SortOrder = sortOrder;
                Year = year;
                ShowYear = year.HasValue;
                SpansMultipleYears = spansMultipleYears;
            }
        }

        public class Item
        {
            public string Location { get; private set; }
            public int CashgameId { get; private set; }
            public Time Duration { get; private set; }
            public Date Date { get; private set; }
            public Money Turnover { get; private set; }
            public Money AverageBuyin { get; private set; }
            public int PlayerCount { get; private set; }

            public Item(Bunch bunch, Cashgame cashgame)
            {
                Location = cashgame.Location;
                CashgameId = cashgame.Id;
                Duration = Time.FromMinutes(cashgame.Duration);
                Date = cashgame.StartTime.HasValue ? new Date(cashgame.StartTime.Value) : new Date(DateTime.MinValue);
                Turnover = new Money(cashgame.Turnover, bunch.Currency);
                AverageBuyin = new Money(cashgame.AverageBuyin, bunch.Currency);
                PlayerCount = cashgame.PlayerCount;
            }
        }

        public enum SortOrder
        {
            Date,
            PlayerCount,
            Location,
            Duration,
            Turnover,
            AverageBuyin
        }
    }
}