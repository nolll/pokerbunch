using Core.Entities;
using Web.Models.MiscModels;

namespace Web.ModelFactories.MiscModelFactories
{
    public interface IAvatarModelFactory
    {
        AvatarModel Create(string email, AvatarSize size = AvatarSize.Large);
    }
}