using JetBrains.Annotations;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordPostModel
    {
        public string Email { get; [UsedImplicitly] set; }
    }
}