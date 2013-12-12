using Core.Repositories;
using Infrastructure.Factories;
using Web.ModelMappers;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Edit;

namespace Web.Commands.CashgameCommands
{
    public class CashgameCommandProvider : ICashgameCommandProvider
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameFactory _cashgameFactory;
        private readonly ICashgameModelMapper _cashgameModelMapper;

        public CashgameCommandProvider(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            ICashgameFactory cashgameFactory,
            ICashgameModelMapper cashgameModelMapper)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _cashgameFactory = cashgameFactory;
            _cashgameModelMapper = cashgameModelMapper;
        }

        public Command GetEndGameCommand(string slug)
        {
            var homegame = _homegameRepository.GetByName(slug);

            return new EndGameCommand(
                _cashgameRepository,
                homegame);
        }

        public Command GetAddCommand(string slug, AddCashgamePostModel postModel)
        {
            var homegame = _homegameRepository.GetByName(slug);
            return new AddCashgameCommand(_cashgameRepository, _cashgameFactory, homegame, postModel);
        }

        public Command GetEditCommand(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            return new EditCashgameCommand(_homegameRepository, _cashgameRepository, _cashgameModelMapper, slug, dateStr, postModel);
        }
    }
}