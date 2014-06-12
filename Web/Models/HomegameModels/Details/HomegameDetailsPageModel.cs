using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.HomegameModels.Details
{
    public class HomegameDetailsPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string HouseRules { get; set; }
        public bool ShowHouseRules { get; set; }
        public Url EditUrl { get; set; }
        public bool ShowEditLink { get; set; }
    }
}