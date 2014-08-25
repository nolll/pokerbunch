using Web.Annotations;

namespace Web.Models.AuthModels
{
    public class LoginPostModel
    {
        public string LoginName { get; [UsedImplicitly] set; }
        public string Password { get; [UsedImplicitly] set; }
        public bool RememberMe { get; [UsedImplicitly] set; }
        public string ReturnUrl { get; [UsedImplicitly] set; }
    }
}