using Core.Entities;
using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.AddCashgame
{
    public static class AddCashgameInteractor
    {
        public static AddCashgameResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, AddCashgameRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = new Cashgame(bunch.Id, request.Location, GameStatus.Running);
            cashgameRepository.AddGame(bunch, cashgame);

            return new AddCashgameResult(request.Slug);
        }
    }
}