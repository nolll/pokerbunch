using Application.Exceptions;
using Application.Factories;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.AddCashgame
{
    public static class AddCashgameInteractor
    {
        public static AddCashgameResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, AddCashgameRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            AddGame(bunchRepository, cashgameRepository, request);
            return new AddCashgameResult(request.Slug);
        }

        private static void AddGame(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, AddCashgameRequest request)
        {
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var cashgame = CashgameFactory.Create(request.Location, homegame.Id, (int)GameStatus.Running);
            cashgameRepository.AddGame(homegame, cashgame);
        }
    }
}