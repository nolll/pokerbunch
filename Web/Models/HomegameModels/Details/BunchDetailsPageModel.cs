using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Details
{
    public class BunchDetailsPageModel : BunchPageModel
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string HouseRules { get; set; }
        public bool ShowHouseRules { get; set; }
        public Url EditUrl { get; set; }
        public bool ShowEditLink { get; set; }

        public BunchDetailsPageModel(BunchContextResult contextResult)
            : base("Bunch Details", contextResult)
        {
        }
    }
}