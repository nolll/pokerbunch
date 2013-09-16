using System.ComponentModel.DataAnnotations;

namespace Web.Models.HomegameModels.Add
{
    public class AddHomegamePostModel
    {
        [Required(ErrorMessage = "Display Name can't be empty")]
        public string DisplayName { get; set; }

        public string Description { get; set; }
        
        [Required(ErrorMessage = "Currency Symbol can't be empty")]
        public string CurrencySymbol { get; set; }
        
        [Required(ErrorMessage = "Currency Layout can't be empty")]
        public string CurrencyLayout { get; set; }

        [Required(ErrorMessage = "Timezone can't be empty")]
        public string TimeZone { get; set; }
    }
}