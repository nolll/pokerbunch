using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.HomegameModels.Details
{
    public class BunchDetailsPageModel : BunchPageModel
    {
        public string DisplayName { get; }
        public string Description { get; }
        public string HouseRules { get; }
        public bool ShowHouseRules { get; }
        public string EditUrl { get; }
        public bool ShowEditLink { get; }

        public BunchDetailsPageModel(BunchContext.Result contextResult, BunchDetails.Result bunchDetails)
            : base(contextResult)
        {
            DisplayName = bunchDetails.BunchName;
            Description = bunchDetails.Description;
            HouseRules = FormatHouseRules(bunchDetails.HouseRules);
            ShowHouseRules = !string.IsNullOrEmpty(bunchDetails.HouseRules);
            EditUrl = new EditBunchUrl(bunchDetails.Id).Relative;
            ShowEditLink = bunchDetails.CanEdit;
        }

        private static string FormatHouseRules(string houseRules)
        {
            return houseRules?.Trim().Replace("\n\r", "<br />\n\r");
        }

        public override string BrowserTitle => "Bunch Details";

        public override View GetView()
        {
            return new View("~/Views/Pages/BunchDetails/BunchDetails.cshtml");
        }
    }
}