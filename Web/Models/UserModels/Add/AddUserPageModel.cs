using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Add
{
    public class AddUserPageModel : AddUserPostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
    }
}