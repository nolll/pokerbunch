using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Details
{
    public class HomegameDetailsPageModel : PageModel
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string HouseRules { get; set; }
        public bool ShowHouseRules { get; set; }
        public Url EditUrl { get; set; }
        public bool ShowEditLink { get; set; }

        public HomegameDetailsPageModel(BunchContextResult contextResult)
            : base("Homegame Details", contextResult)
        {
        }
    }
}