using Application.Factories;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.AddCashgame
{
    public class AddCashgameInteractor : IAddCashgameInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public AddCashgameInteractor(
            IHomegameRepository homegameRepository, 
            ICashgameRepository cashgameRepository)
        {
            _homegameRepository = homegameRepository;
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
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var cashgame = new CashgameFactory().Create(request.Location, homegame.Id, (int)GameStatus.Running);
            _cashgameRepository.AddGame(homegame, cashgame);
        }
    }
}