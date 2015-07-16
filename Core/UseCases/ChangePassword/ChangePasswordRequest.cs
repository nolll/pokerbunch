using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.ChangePassword
{
    public class ChangePasswordRequest
    {
        public string UserName { get; private set; }
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; private set; }
        public string Repeat { get; private set; }

        public ChangePasswordRequest(string userName, string password, string repeat)
        {
            UserName = userName;
            Password = password;
            Repeat = repeat;
        }
    }
}