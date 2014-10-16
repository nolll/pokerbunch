using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.EditUser
{
    public class EditUserRequest
    {
        public string UserName { get; private set; }
        [Required(ErrorMessage = "Display Name can't be empty")]
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        public string Email { get; private set; }

        public EditUserRequest(string userName, string displayName, string realName, string email)
        {
            UserName = userName;
            DisplayName = displayName;
            RealName = realName;
            Email = email;
        }
    }
}