using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerPageModel : AddPlayerPostModel, IPageModel
    {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }

	}

}