using Core.Entities;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IInvitePlayerPageBuilder
    {
        InvitePlayerPageModel Build(Homegame homegame, InvitePlayerPostModel postModel);
    }
}