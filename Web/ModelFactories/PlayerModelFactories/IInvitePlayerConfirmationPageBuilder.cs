using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IInvitePlayerConfirmationPageBuilder
    {
        InvitePlayerConfirmationPageModel Build(string slug);
    }
}