using Core.Classes;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IInvitePlayerPageModelFactory
    {
        InvitePlayerPageModel Create(User user, Homegame homegame, InvitePlayerPostModel postModel);
    }
}