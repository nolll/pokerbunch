using System.ComponentModel.DataAnnotations;
using Web.Annotations;

namespace Web.Models.HomegameModels.Edit
{
    public class EditBunchPostModel
    {
        public string Description { get; [UsedImplicitly] set; }
        
        [Required(ErrorMessage = "Currency Symbol can't be empty")]
        public string CurrencySymbol { get; [UsedImplicitly] set; }
        
        [Required(ErrorMessage = "Currency Layout can't be empty")]
        public string CurrencyLayout { get; [UsedImplicitly] set; }

        [Required(ErrorMessage = "Timezone can't be empty")]
        public string TimeZone { get; [UsedImplicitly] set; }

        public string HouseRules { get; [UsedImplicitly] set; }

        public int DefaultBuyin { get; [UsedImplicitly] set; }

        /*
        public bool CashgamesEnabled { get; [UsedImplicitly] set; }
        public bool TournamentsEnabled { get; [UsedImplicitly] set; }
        public bool VideosEnabled { get; [UsedImplicitly] set; }
        */
    }
}