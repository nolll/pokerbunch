using Core.Repositories;
using Web.ModelMappers;
using Web.Models.CashgameModels.Edit;

namespace Web.Commands.CashgameCommands
{
    public class EditCashgameCommand : Command
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameModelMapper _cashgameModelMapper;
        private readonly string _slug;
        private readonly string _dateStr;
        private readonly CashgameEditPostModel _model;

        public EditCashgameCommand(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            ICashgameModelMapper cashgameModelMapper,
            string slug,
            string dateStr,
            CashgameEditPostModel model)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _cashgameModelMapper = cashgameModelMapper;
            _slug = slug;
            _dateStr = dateStr;
            _model = model;
        }

        public override bool Execute()
        {
            if (!IsValid(_model)) return false;
            if (!_model.HasLocation)
            {
                AddError("Please enter a location");
                return false;
            }
            var homegame = _homegameRepository.GetByName(_slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, _dateStr);
            cashgame = _cashgameModelMapper.Map(cashgame, _model);
            _cashgameRepository.UpdateGame(cashgame);
            return true;
        }
    }
}