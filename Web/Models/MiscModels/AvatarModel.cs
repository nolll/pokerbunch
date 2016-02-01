namespace Web.Models.MiscModels
{
    public class AvatarModel
    {
        public string AvatarUrl { get; }

        public AvatarModel()
            : this(string.Empty)
        {
        }

        public AvatarModel(string avatarUrl)
        {
            AvatarUrl = avatarUrl;
        }

        public bool AvatarEnabled
        {
            get { return !string.IsNullOrEmpty(AvatarUrl); }
        }
    }
}