using Web.Annotations;

namespace Web.Models.UserModels.Edit
{
    public class EditUserPostModel
    {
        public string DisplayName { get; [UsedImplicitly] set; }
        public string RealName { get; [UsedImplicitly] set; }
        public string Email { get; [UsedImplicitly] set; }
    }
}