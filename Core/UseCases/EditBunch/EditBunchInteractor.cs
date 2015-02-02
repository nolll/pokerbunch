using System;
using Core.Entities;
using Core.Repositories;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.EditBunch
{
    public class EditBunchInteractor
    {
        private readonly IBunchRepository _bunchRepository;

        public EditBunchInteractor(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public EditBunchResult Execute(EditBunchRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var postedHomegame = CreateBunch(bunch, request);
            _bunchRepository.Save(postedHomegame);

            var returnUrl = new BunchDetailsUrl(request.Slug);
            return new EditBunchResult(returnUrl);
        }

        private static Bunch CreateBunch(Bunch bunch, EditBunchRequest request)
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
    }
}
