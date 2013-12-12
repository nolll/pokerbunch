using Core.Classes;
using Core.Repositories;
using Infrastructure.Factories;
using Web.Models.CashgameModels.Add;

namespace Web.Commands.CashgameCommands
{
    public class AddCashgameCommand : Command
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameFactory _cashgameFactory;
        private readonly Homegame _homegame;
        private readonly AddCashgamePostModel _model;

        public AddCashgameCommand(ICashgameRepository cashgameRepository, ICashgameFactory cashgameFactory, Homegame homegame, AddCashgamePostModel model)
        {
            _cashgameRepository = cashgameRepository;
            _cashgameFactory = cashgameFactory;
            _homegame = homegame;
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
            var cashgame = _cashgameFactory.Create(_model.Location, (int)GameStatus.Running);
            _cashgameRepository.AddGame(_homegame, cashgame);
            return true;
        }

    }
}