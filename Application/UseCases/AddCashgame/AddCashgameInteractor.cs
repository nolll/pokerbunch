using Application.Factories;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.AddCashgame
{
    public class AddCashgameInteractor : IAddCashgameInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameFactory _cashgameFactory;

        public AddCashgameInteractor(IHomegameRepository homegameRepository, ICashgameRepository cashgameRepository, ICashgameFactory cashgameFactory)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _cashgameFactory = cashgameFactory;
        }

        public AddCashgameResult Execute(AddCashgameRequest request)
        {
            var validator = new Validator(request);

            if (!request.HasLocation)
                validator.AddError("Please enter a location");

            if (!validator.IsValid)
                return new AddCashgameResult(validator);
            
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameFactory.Create(request.Location, homegame.Id, (int)GameStatus.Running);
            _cashgameRepository.AddGame(homegame, cashgame);

            return new AddCashgameResult(validator);
        }
    }
}