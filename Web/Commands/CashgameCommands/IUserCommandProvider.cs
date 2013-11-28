using Core.Classes;

namespace Web.Commands.CashgameCommands
{
    public interface ICashgameCommandProvider
    {
        Command GetEndGameCommand(Homegame homegame);
    }
}