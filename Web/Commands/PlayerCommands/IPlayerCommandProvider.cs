using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;

namespace Web.Commands.PlayerCommands
{
    public interface IPlayerCommandProvider
    {
        Command GetInviteCommand(string slug, int playerId, InvitePlayerPostModel model);
        Command GetAddCommand(string slug, AddPlayerPostModel model);
        Command GetDeleteCommand(string slug, int playerId);
    }
}