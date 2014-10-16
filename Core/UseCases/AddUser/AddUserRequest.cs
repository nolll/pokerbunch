using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.AddUser
{
    public class AddUserRequest
    {
        [Required(ErrorMessage = "Login Name can't be empty")]
        public string UserName { get; private set; }

        [Required(ErrorMessage = "Display Name can't be empty")]
        public string DisplayName { get; private set; }

        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        public string Email { get; private set; }

        public AddUserRequest(string userName, string displayName, string email)
        {
            UserName = userName;
            DisplayName = displayName;
            Email = email;
        }
    }
}