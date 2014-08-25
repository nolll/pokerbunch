using System.ComponentModel.DataAnnotations;
using Web.Annotations;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordPostModel
    {
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; [UsedImplicitly] set; }
        public string Repeat { get; [UsedImplicitly] set; }
    }
}