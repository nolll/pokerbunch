using Web.Annotations;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordPostModel
    {
        public string Password { get; [UsedImplicitly] set; }
        public string Repeat { get; [UsedImplicitly] set; }
    }
}