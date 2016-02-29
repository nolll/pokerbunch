namespace Web.Models.MiscModels
{
    public class AvatarModel
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
    }
}