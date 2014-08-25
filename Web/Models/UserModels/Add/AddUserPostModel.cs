using System.ComponentModel.DataAnnotations;
using Web.Annotations;

namespace Web.Models.UserModels.Add
{
    public class AddUserPostModel
    {
        [Required(ErrorMessage = "Login Name can't be empty")]
        public string UserName { get; [UsedImplicitly] set; }
        
        [Required(ErrorMessage = "Display Name can't be empty")]
        public string DisplayName { get; [UsedImplicitly] set; }

        [Required(ErrorMessage = "Email can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The email address is not valid")]
        public string Email { get; [UsedImplicitly] set; }
    }
}