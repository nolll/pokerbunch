using System.Collections.Generic;
using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.List
{
	public class PlayerListPageModel : PageModel
    {
        public IList<PlayerItemModel> PlayerModels { get; set; }
	    public Url AddUrl { get; set; }
	    public bool ShowAddLink { get; set; }

        public PlayerListPageModel(BunchContextResult contextResult)
            : base("Player List", contextResult)
	    {
	    }
    }
}