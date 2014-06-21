using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
    }
}