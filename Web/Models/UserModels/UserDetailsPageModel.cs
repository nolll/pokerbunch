using Application.Urls;
using Application.UseCases.AppContext;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels
{
    public class UserDetailsPageModel : PageModel
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public Url EditUrl { get; set; }
        public Url ChangePasswordUrl { get; set; }
        public bool ShowEditLink { get; set; }
        public bool ShowPasswordLink { get; set; }
        public AvatarModel AvatarModel { get; set; }

        public UserDetailsPageModel(AppContextResult contextResult)
            : base("User Details", contextResult)
        {
        }
    }
}