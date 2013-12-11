using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;

namespace Web.Commands.PlayerCommands
{
    public interface IPlayerCommandProvider
    {
        Command GetInviteCommand(string slug, string playerName, InvitePlayerPostModel model);
        Command GetAddCommand(string slug, AddPlayerPostModel model);
        Command GetDeleteCommand(string slug, string playerName);
    }
}