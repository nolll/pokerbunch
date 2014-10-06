using Core.Urls;

namespace Core.UseCases.BunchDetails
{
    public class BunchDetailsResult
    {
        public string BunchName { get; private set; }
        public string Description { get; private set; }
        public string HouseRules { get; private set; }
        public Url EditBunchUrl { get; private set; }
        public bool CanEdit { get; private set; }

        public BunchDetailsResult(string bunchName, string description, string houseRules, Url editBunchUrl, bool canEdit)
        {
            BunchName = bunchName;
            Description = description;
            HouseRules = houseRules;
            EditBunchUrl = editBunchUrl;
            CanEdit = canEdit;
        }
    }
}