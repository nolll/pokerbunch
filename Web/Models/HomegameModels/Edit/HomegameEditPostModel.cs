using System.ComponentModel.DataAnnotations;

namespace Web.Models.HomegameModels.Edit
{
    public class HomegameEditPostModel
    {
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Currency Symbol can't be empty")]
        public string CurrencySymbol { get; set; }
        
        [Required(ErrorMessage = "Currency Layout can't be empty")]
        public string CurrencyLayout { get; set; }

        [Required(ErrorMessage = "Timezone can't be empty")]
        public string TimeZone { get; set; }

        public string HouseRules { get; set; }

        public int DefaultBuyin { get; set; }
        
        /*
        public bool CashgamesEnabled { get; set; }
        public bool TournamentsEnabled { get; set; }
        public bool VideosEnabled { get; set; }
        */
    }
}