using JetBrains.Annotations;

namespace Web.Models.AuthModels
{
    public class LoginPostModel
    {
        public string Username { get; [UsedImplicitly] set; }
        public string Password { get; [UsedImplicitly] set; }
        public bool RememberMe { get; [UsedImplicitly] set; }
    }
}