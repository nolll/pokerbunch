using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.UserModels
{
    public class UserDetailsPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public UrlModel EditLink { get; set; }
        public string ChangePasswordLink { get; set; }
        public bool ShowEditLink { get; set; }
        public bool ShowPasswordLink { get; set; }
        public AvatarModel AvatarModel { get; set; }
    }
}