using System.ComponentModel.DataAnnotations;
using Web.Annotations;

namespace Web.Models.UserModels.Edit
{
    public class EditUserPostModel
    {
        [Required(ErrorMessage = "Display Name can't be empty")]
        public string DisplayName { get; [UsedImplicitly] set; }

        public string RealName { get; [UsedImplicitly] set; }

        [Required(ErrorMessage = "Email can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The email address is not valid")]
        public string Email { get; [UsedImplicitly] set; }
    }
}