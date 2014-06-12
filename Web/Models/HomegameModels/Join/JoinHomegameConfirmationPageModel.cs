using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinHomegameConfirmationPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public string BunchName { get; set; }
        public Url BunchUrl { get; set; }
    }
}