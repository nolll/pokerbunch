using Application.Urls;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.SharingModels
{
	public class SharingTwitterPageModel : IPageModel
    {
        public PageProperties PageProperties { get; set; }
	    public string BrowserTitle { get; set; }
		public string TwitterName { get; set; }
		public bool IsSharing { get; set; }
		public Url PostUrl { get; set; }
    }
}