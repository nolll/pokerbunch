using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.ChangePassword
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; private set; }
        public string Repeat { get; private set; }

        public ChangePasswordRequest(string password, string repeat)
        {
            Password = password;
            Repeat = repeat;
        }
    }
}