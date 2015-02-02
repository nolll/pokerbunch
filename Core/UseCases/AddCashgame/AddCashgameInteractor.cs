using Core.Entities;
using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.AddCashgame
{
    public class AddCashgameInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public AddCashgameInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public AddCashgameResult Execute(AddCashgameRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = new Cashgame(bunch.Id, request.Location, GameStatus.Running);
            _cashgameRepository.AddGame(bunch, cashgame);

            return new AddCashgameResult(request.Slug);
        }
    }
}