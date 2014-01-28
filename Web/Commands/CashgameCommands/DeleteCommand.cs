using Core.Classes;
using Core.Repositories;

namespace Web.Commands.CashgameCommands
{
    public class DeleteCommand : Command
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly Cashgame _cashgame;

        public DeleteCommand(
            ICashgameRepository cashgameRepository,
            Cashgame cashgame)
        {
            _cashgameRepository = cashgameRepository;
            _cashgame = cashgame;
        }

        public override bool Execute()
        {
            if (_cashgame.PlayerCount > 0)
            {
                AddError("Cashgames with results can't be deleted.");
                return false;
            }
            _cashgameRepository.DeleteGame(_cashgame);
            return true;
        }
    }
}