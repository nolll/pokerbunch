using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class EditBunch
    {
        private readonly BunchService _bunchService;

        public EditBunch(BunchService bunchService)
        {
            _bunchService = bunchService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.Slug);
            var postedHomegame = CreateBunch(bunch, request);
            _bunchService.Save(postedHomegame);

            return new Result(bunch.Id, bunch.Slug);
        }

        private static Bunch CreateBunch(Bunch bunch, Request request)
        {
            return new Bunch(
                bunch.Id,
                bunch.Slug,
                bunch.DisplayName,
                request.Description,
                request.HouseRules,
                TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone),
                request.DefaultBuyin,
                new Currency(request.CurrencySymbol, request.CurrencyLayout));
        }

        public class Request
        {
            public string UserName { get; }
            public string Slug { get; }
            public string Description { get; }
            [Required(ErrorMessage = "Currency Symbol can't be empty")]
            public string CurrencySymbol { get; }
            [Required(ErrorMessage = "Currency Layout can't be empty")]
            public string CurrencyLayout { get; }
            [Required(ErrorMessage = "Timezone can't be empty")]
            public string TimeZone { get; }
            public string HouseRules { get; }
            public int DefaultBuyin { get; }

            public Request(string userName, string slug, string description, string currencySymbol, string currencyLayout, string timeZone, string houseRules, int defaultBuyin)
            {
                UserName = userName;
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
            public string Slug { get; private set; }

            public Result(string bunchId, string slug)
            {
                BunchId = bunchId;
                Slug = slug;
            }
        }
    }
}
