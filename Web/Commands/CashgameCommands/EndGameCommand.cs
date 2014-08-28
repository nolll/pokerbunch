using Core.Entities;
using Core.Repositories;

namespace Web.Commands.CashgameCommands
{
    public class EndGameCommand : Command
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly Bunch _bunch;
        private readonly Cashgame _cashgame;

        public EndGameCommand(
            ICashgameRepository cashgameRepository,
            Bunch bunch,
            Cashgame cashgame)
        {
            _cashgameRepository = cashgameRepository;
            _bunch = bunch;
            _cashgame = cashgame;
        }

        public override bool Execute()
        {
            _cashgameRepository.EndGame(_bunch, _cashgame);
            return true;
        }
    }
}