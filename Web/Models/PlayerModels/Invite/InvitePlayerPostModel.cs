using Web.Annotations;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerPostModel
    {
        public string Email { get; [UsedImplicitly] set; }
	}
}