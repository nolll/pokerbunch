using Core.Entities;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.CashgameModels.Edit;

namespace Web.Commands.CashgameCommands
{
    public class EditCashgameCommand : Command
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly string _slug;
        private readonly string _dateStr;
        private readonly CashgameEditPostModel _model;

        public EditCashgameCommand(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            string slug,
            string dateStr,
            CashgameEditPostModel model)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
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
            var bunch = _bunchRepository.GetBySlug(_slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch, _dateStr);
            cashgame = new Cashgame(cashgame.BunchId, _model.Location, cashgame.Status, cashgame.Id, cashgame.Results);
            _cashgameRepository.UpdateGame(cashgame);
            return true;
        }
    }
}