using System;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.AddBunch
{
    public class AddBunchInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public AddBunchInteractor(IUserRepository userRepository, IBunchRepository bunchRepository, IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public AddBunchResult Execute(AddBunchRequest request)
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
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = new Player(id, user.Id, Role.Manager);
            _playerRepository.Add(player);

            var returnUrl = new AddBunchConfirmationUrl();
            return new AddBunchResult(returnUrl);
        }

        private static Bunch CreateBunch(AddBunchRequest request)
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
    }
}
