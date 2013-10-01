using System.ComponentModel.DataAnnotations;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordPostModel
    {
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; set; }
        public string Repeat { get; set; }
    }
}