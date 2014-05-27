using Web.Models.PageBaseModels;

namespace Web.Models.SharingModels
{
	public class SharingTwitterPageModel : IPageModel
    {
        public PageProperties PageProperties { get; set; }
	    public string BrowserTitle { get; set; }
		public string TwitterName { get; set; }
		public bool IsSharing { get; set; }
		public string PostUrl { get; set; }
    }
}