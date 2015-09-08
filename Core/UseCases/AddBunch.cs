using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class AddBunch
    {
        private readonly UserService _userService;
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public AddBunch(UserService userService, IBunchRepository bunchRepository, IPlayerRepository playerRepository)
        {
            _userService = userService;
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var slug = SlugGenerator.GetSlug(request.DisplayName);

            bool bunchExists;
            try
            {
                var b = _bunchRepository.GetBySlug(slug);
                bunchExists = true;
            }
            catch (BunchNotFoundException)
            {
                bunchExists = false;
            }

            if (bunchExists)
                throw new BunchExistsException();

            var bunch = CreateBunch(request);
            var id = _bunchRepository.Add(bunch);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = new Player(id, user.Id, Role.Manager);
            _playerRepository.Add(player);

            return new Result(bunch.Id);
        }

        private static Bunch CreateBunch(Request request)
        {
            return new Bunch(
                0,
                SlugGenerator.GetSlug(request.DisplayName),
                request.DisplayName,
                request.Description,
                string.Empty,
                TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone),
                200,
                new Currency(request.CurrencySymbol, request.CurrencyLayout));
        }

        public class Request
        {
            public string UserName { get; private set; }
            [Required(ErrorMessage = "Display Name can't be empty")]
            public string DisplayName { get; private set; }
            public string Description { get; private set; }
            [Required(ErrorMessage = "Currency Symbol can't be empty")]
            public string CurrencySymbol { get; private set; }
            [Required(ErrorMessage = "Currency Layout can't be empty")]
            public string CurrencyLayout { get; private set; }
            [Required(ErrorMessage = "Timezone can't be empty")]
            public string TimeZone { get; private set; }

            public Request(string userName, string displayName, string description, string currencySymbol, string currencyLayout, string timeZone)
            {
                UserName = userName;
                DisplayName = displayName;
                Description = description;
                CurrencySymbol = currencySymbol;
                CurrencyLayout = currencyLayout;
                TimeZone = timeZone;
            }
        }

        public class Result
        {
            public int BunchId { get; private set; }

            public Result(int bunchId)
            {
                BunchId = bunchId;
            }
        }
    }
}
