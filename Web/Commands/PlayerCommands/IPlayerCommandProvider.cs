using Core.Classes;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;

namespace Web.Commands.PlayerCommands
{
    public interface IPlayerCommandProvider
    {
        Command GetInviteCommand(Homegame homegame, Player player, InvitePlayerPostModel model);
        Command GetAddCommand(Homegame homegame, AddPlayerPostModel model);
    }
}