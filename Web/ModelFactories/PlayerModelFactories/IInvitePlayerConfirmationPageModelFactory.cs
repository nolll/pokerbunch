using Core.Entities;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IInvitePlayerConfirmationPageModelFactory
    {
        InvitePlayerConfirmationPageModel Create(Homegame homegame);
    }
}