using System.ComponentModel.DataAnnotations;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordPostModel
    {
        [Required(ErrorMessage = "Email can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The email address is not valid")]
        public string Email { get; set; }
    }
}