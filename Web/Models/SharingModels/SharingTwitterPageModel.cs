using Application.Urls;
using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.SharingModels
{
	public class SharingTwitterPageModel : PageModel
    {
		public string TwitterName { get; set; }
		public bool IsSharing { get; set; }
		public Url PostUrl { get; set; }

        public SharingTwitterPageModel(AppContextResult contextResult)
            : base("Twitter Sharing", contextResult)
	    {
	    }
    }
}