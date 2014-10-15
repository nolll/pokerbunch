using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.EditBunch
{
    public class EditBunchRequest
    {
        public string Slug { get; private set; }
        public string Description { get; private set; }
        [Required(ErrorMessage = "Currency Symbol can't be empty")]
        public string CurrencySymbol { get; private set; }
        [Required(ErrorMessage = "Currency Layout can't be empty")]
        public string CurrencyLayout { get; private set; }
        [Required(ErrorMessage = "Timezone can't be empty")]
        public string TimeZone { get; private set; }
        public string HouseRules { get; private set; }
        public int DefaultBuyin { get; private set; }

        public EditBunchRequest(string slug, string description, string currencySymbol, string currencyLayout, string timeZone, string houseRules, int defaultBuyin)
        {
            Slug = slug;
            Description = description;
            CurrencySymbol = currencySymbol;
            CurrencyLayout = currencyLayout;
            TimeZone = timeZone;
            HouseRules = houseRules;
            DefaultBuyin = defaultBuyin;
        }
    }
}