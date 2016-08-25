using Core.UseCases;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Details
{
    public class BunchDetailsPageModel : BunchPageModel
    {
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public string HouseRules { get; private set; }
        public bool ShowHouseRules { get; private set; }
        public string EditUrl { get; private set; }
        public bool ShowEditLink { get; private set; }

        public BunchDetailsPageModel(BunchContext.Result contextResult, BunchDetails.Result bunchDetails)
            : base(contextResult)
        {
            DisplayName = bunchDetails.BunchName;
            Description = bunchDetails.Description;
            HouseRules = FormatHouseRules(bunchDetails.HouseRules);
            ShowHouseRules = !string.IsNullOrEmpty(bunchDetails.HouseRules);
            EditUrl = new EditBunchUrl(bunchDetails.Slug).Relative;
            ShowEditLink = bunchDetails.CanEdit;
        }

        public override string BrowserTitle => "Bunch Details";

        private string FormatHouseRules(string houseRules)
        {
            return houseRules?.Trim().Replace("\n\r", "<br />\n\r");
        }
    }
}