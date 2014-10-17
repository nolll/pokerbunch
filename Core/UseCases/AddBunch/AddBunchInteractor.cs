using System;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.AddBunch
{
    public static class AddBunchInteractor
    {
        public static AddBunchResult Execute(IAuth auth, IBunchRepository bunchRepository, IPlayerRepository playerRepository, AddBunchRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var slug = SlugGenerator.GetSlug(request.DisplayName);
            var existingBunch = bunchRepository.GetBySlug(slug);

            if (existingBunch != null)
            {
                throw new BunchExistsException();
            }
            //todo: test the following 4 lines
            var bunch = CreateBunch(request);
            bunch = bunchRepository.Add(bunch);
            var user = auth.CurrentUser;
            playerRepository.Add(bunch, user, Role.Manager);

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
