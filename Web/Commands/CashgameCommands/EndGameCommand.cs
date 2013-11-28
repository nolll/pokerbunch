using Core.Classes;
using Core.Repositories;

namespace Web.Commands.CashgameCommands
{
    public class EndGameCommand : Command
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly Homegame _homegame;

        public EndGameCommand(
            ICashgameRepository cashgameRepository,
            Homegame homegame)
        {
            _cashgameRepository = cashgameRepository;
            _homegame = homegame;
        }

        public override bool Execute()
        {
            var cashgame = _cashgameRepository.GetRunning(_homegame);
            _cashgameRepository.EndGame(cashgame);
            _cashgameRepository.ClearCashgameFromCache(cashgame);
            _cashgameRepository.ClearCashgameListFromCache(_homegame, cashgame);
            return true;
        }
    }
}