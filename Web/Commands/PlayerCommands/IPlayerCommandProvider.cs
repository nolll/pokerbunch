using Web.Models.PlayerModels.Add;

namespace Web.Commands.PlayerCommands
{
    public interface IPlayerCommandProvider
    {
        Command GetAddCommand(string slug, AddPlayerPostModel model);
        Command GetDeleteCommand(string slug, int playerId);
    }
}