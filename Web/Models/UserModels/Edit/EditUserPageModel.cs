using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Edit
{
    public class EditUserPageModel : EditUserPostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public string UserName { get; set; }
	}
}