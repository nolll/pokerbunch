using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.ChangePassword
{
    public class ChangePasswordRequest
    {
        public int UserId { get; private set; }
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; private set; }
        public string Repeat { get; private set; }

        public ChangePasswordRequest(int userId, string password, string repeat)
        {
            UserId = userId;
            Password = password;
            Repeat = repeat;
        }
    }
}