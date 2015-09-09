using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditBunch
    {
        private readonly BunchService _bunchService;
        private readonly UserService _userService;
        private readonly IPlayerRepository _playerRepository;

        public EditBunch(BunchService bunchService, UserService userService, IPlayerRepository playerRepository)
        {
            _bunchService = bunchService;
            _userService = userService;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchService.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequireManager(user, player);
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
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public string Description { get; private set; }
            [Required(ErrorMessage = "Currency Symbol can't be empty")]
            public string CurrencySymbol { get; private set; }
            [Required(ErrorMessage = "Currency Layout can't be empty")]
            public string CurrencyLayout { get; private set; }
            [Required(ErrorMessage = "Timezone can't be empty")]
            public string TimeZone { get; private set; }
            public string HouseRules { get; private set; }
            public int DefaultBuyin { get; private set; }

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
            public int BunchId { get; private set; }
            public string Slug { get; private set; }

            public Result(int bunchId, string slug)
            {
                BunchId = bunchId;
                Slug = slug;
            }
        }
    }
}
