using Core.Classes;
using Core.Repositories;

namespace Web.Commands.CashgameCommands
{
    public class CashgameCommandProvider : ICashgameCommandProvider
    {
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameCommandProvider(
            ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public Command GetEndGameCommand(Homegame homegame)
        {
            return new EndGameCommand(
                _cashgameRepository,
                homegame);
        }

    }
}