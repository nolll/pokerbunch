using Core.Entities;
using Core.Repositories;

namespace Web.Commands.CashgameCommands
{
    public class EndGameCommand : Command
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly Homegame _homegame;
        private readonly Cashgame _cashgame;

        public EndGameCommand(
            ICashgameRepository cashgameRepository,
            Homegame homegame,
            Cashgame cashgame)
        {
            _cashgameRepository = cashgameRepository;
            _homegame = homegame;
            _cashgame = cashgame;
        }

        public override bool Execute()
        {
            _cashgameRepository.EndGame(_homegame, _cashgame);
            return true;
        }
    }
}