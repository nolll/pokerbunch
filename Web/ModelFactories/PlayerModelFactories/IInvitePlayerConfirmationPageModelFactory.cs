using Core.Classes;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IInvitePlayerConfirmationPageModelFactory
    {
        InvitePlayerConfirmationPageModel Create(User user, Homegame homegame);
    }
}