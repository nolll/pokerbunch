using Core.Classes;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IInvitePlayerConfirmationPageModelFactory
    {
        InvitePlayerConfirmationPageModel Create(User user, Homegame homegame, Cashgame runningGame);
    }
}