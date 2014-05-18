using Core.Entities;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IInvitePlayerPageModelFactory
    {
        InvitePlayerPageModel Create(Homegame homegame, InvitePlayerPostModel postModel);
    }
}