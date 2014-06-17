using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IInvitePlayerPageBuilder
    {
        InvitePlayerPageModel Build(string slug, InvitePlayerPostModel postModel = null);
    }
}