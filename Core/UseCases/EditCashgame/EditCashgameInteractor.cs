using Core.Entities;
using Core.Repositories;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.EditCashgame
{
    public static class EditCashgameInteractor
    {
        public static EditCashgameResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, EditCashgameRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            cashgame = new Cashgame(cashgame.BunchId, request.Location, cashgame.Status, cashgame.Id, cashgame.Results);
            cashgameRepository.UpdateGame(cashgame);
            
            var returnUrl = new CashgameDetailsUrl(request.Slug, request.DateStr);

            return new EditCashgameResult(returnUrl);
        }
    }
}
