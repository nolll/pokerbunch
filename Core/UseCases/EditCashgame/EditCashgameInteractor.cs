using Core.Entities;
using Core.Repositories;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.EditCashgame
{
    public class EditCashgameInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public EditCashgameInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public EditCashgameResult Execute(EditCashgameRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            cashgame = new Cashgame(cashgame.BunchId, request.Location, cashgame.Status, cashgame.Id);
            _cashgameRepository.UpdateGame(cashgame);
            
            var returnUrl = new CashgameDetailsUrl(request.Slug, request.DateStr);

            return new EditCashgameResult(returnUrl);
        }
    }
}
