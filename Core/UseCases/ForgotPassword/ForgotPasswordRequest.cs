using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.ForgotPassword
{
    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        public string Email { get; private set; }

        public ForgotPasswordRequest(string email)
        {
            Email = email;
        }
    }
}