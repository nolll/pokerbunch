using System.ComponentModel.DataAnnotations;
using Web.Annotations;

namespace Web.Models.HomegameModels.Add
{
    public class AddHomegamePostModel
    {
        [Required(ErrorMessage = "Display Name can't be empty")]
        public string DisplayName { get; [UsedImplicitly] set; }

        public string Description { get; [UsedImplicitly] set; }
        
        [Required(ErrorMessage = "Currency Symbol can't be empty")]
        public string CurrencySymbol { get; [UsedImplicitly] set; }
        
        [Required(ErrorMessage = "Currency Layout can't be empty")]
        public string CurrencyLayout { get; [UsedImplicitly] set; }

        [Required(ErrorMessage = "Timezone can't be empty")]
        public string TimeZone { get; [UsedImplicitly] set; }
    }
}