using Web.Extensions;

namespace Web.Models.MiscModels
{
    public class AvatarModel : IViewModel
    {
        public string AvatarUrl { get; }
        public bool AvatarEnabled => !string.IsNullOrEmpty(AvatarUrl);

        public AvatarModel()
            : this(string.Empty)
        {
        }

        public AvatarModel(string avatarUrl)
        {
            AvatarUrl = avatarUrl;
        }

        public View GetView()
        {
            return new View("~/Views/Misc/Avatar.cshtml");
        }
    }
}