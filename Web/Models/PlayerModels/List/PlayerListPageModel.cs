using System.Collections.Generic;
using Application.Urls;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.PlayerModels.List
{
	public class PlayerListPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public IList<PlayerItemModel> PlayerModels { get; set; }
	    public Url AddUrl { get; set; }
	    public bool ShowAddLink { get; set; }
    }
}