using Web.Annotations;

namespace Web.Models.UserModels.Add
{
    public class AddUserPostModel
    {
        public string UserName { get; [UsedImplicitly] set; }
        public string DisplayName { get; [UsedImplicitly] set; }
        public string Email { get; [UsedImplicitly] set; }
    }
}