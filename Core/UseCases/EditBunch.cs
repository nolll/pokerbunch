using System;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class EditBunch
    {
        private readonly IBunchRepository _bunchRepository;

        public EditBunch(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.Slug);
            var postedHomegame = CreateBunch(bunch, request);
            _bunchRepository.Update(postedHomegame);

            return new Result(bunch.Id);
        }

        private static Bunch CreateBunch(Bunch bunch, Request request)
        {
            return new Bunch(
                bunch.Id,
                bunch.DisplayName,
                request.Description,
                request.HouseRules,
                TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone),
                request.DefaultBuyin,
                new Currency(request.CurrencySymbol, request.CurrencyLayout));
        }

        public class Request
        {
            public string Slug { get; }
            public string Description { get; }
            public string CurrencySymbol { get; }
            public string CurrencyLayout { get; }
            public string TimeZone { get; }
            public string HouseRules { get; }
            public int DefaultBuyin { get; }

            public Request(string slug, string description, string currencySymbol, string currencyLayout, string timeZone, string houseRules, int defaultBuyin)
            {
                Slug = slug;
                Description = description;
                CurrencySymbol = currencySymbol;
                CurrencyLayout = currencyLayout;
                TimeZone = timeZone;
                HouseRules = houseRules;
                DefaultBuyin = defaultBuyin;
            }
        }

        public class Result
        {
            public string BunchId { get; private set; }

            public Result(string bunchId)
            {
                BunchId = bunchId;
            }
        }
    }
}
