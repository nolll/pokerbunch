using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.AddBunch
{
    public class AddBunchRequest
    {
        [Required(ErrorMessage = "Display Name can't be empty")]
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        [Required(ErrorMessage = "Currency Symbol can't be empty")]
        public string CurrencySymbol { get; private set; }
        [Required(ErrorMessage = "Currency Layout can't be empty")]
        public string CurrencyLayout { get; private set; }
        [Required(ErrorMessage = "Timezone can't be empty")]
        public string TimeZone { get; private set; }

        public AddBunchRequest(string displayName, string description, string currencySymbol, string currencyLayout, string timeZone)
        {
            DisplayName = displayName;
            Description = description;
            CurrencySymbol = currencySymbol;
            CurrencyLayout = currencyLayout;
            TimeZone = timeZone;
        }
    }
}