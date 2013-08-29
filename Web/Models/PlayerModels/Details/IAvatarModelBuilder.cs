using Core.Classes;

namespace Web.Models.PlayerModels.Details
{
    public interface IAvatarModelBuilder
    {
        AvatarModel Build(string email, AvatarSize size = AvatarSize.Large);
    }
}