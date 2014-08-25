using System.ComponentModel.DataAnnotations;
using Web.Annotations;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerPostModel
    {
        [Required(ErrorMessage = "Email can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The email address is not valid")]
        public string Email { get; [UsedImplicitly] set; }
	}
}