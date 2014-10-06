using Core.UseCases.BunchContext;
using Core.UseCases.BunchDetails;
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

        public BunchDetailsPageModel(BunchContextResult contextResult, BunchDetailsResult bunchDetailsResult)
            : base("Bunch Details", contextResult)
        {
            DisplayName = bunchDetailsResult.BunchName;
            Description = bunchDetailsResult.Description;
            HouseRules = FormatHouseRules(bunchDetailsResult.HouseRules);
            ShowHouseRules = !string.IsNullOrEmpty(bunchDetailsResult.HouseRules);
            EditUrl = bunchDetailsResult.EditBunchUrl.Relative;
            ShowEditLink = bunchDetailsResult.CanEdit;
        }

        private string FormatHouseRules(string houseRules)
        {
            return houseRules != null ? houseRules.Trim().Replace("\n\r", "<br />\n\r") : null;
        }
    }
}