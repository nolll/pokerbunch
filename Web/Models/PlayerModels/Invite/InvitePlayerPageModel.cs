using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerPageModel : InvitePlayerPostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
	}
}