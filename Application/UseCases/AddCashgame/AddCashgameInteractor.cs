using Application.Factories;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.AddCashgame
{
    public class AddCashgameInteractor : IAddCashgameInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public AddCashgameInteractor(
            IBunchRepository bunchRepository, 
            ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public AddCashgameResult Execute(AddCashgameRequest request)
        {
            var validator = new Validator(request);

            if (validator.IsValid)
                AddGame(request);
            
            return new AddCashgameResult(request.Slug, validator);
        }

        private void AddGame(AddCashgameRequest request)
        {
            var homegame = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = CashgameFactory.Create(request.Location, homegame.Id, (int)GameStatus.Running);
            _cashgameRepository.AddGame(homegame, cashgame);
        }
    }
}