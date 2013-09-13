using System.ComponentModel.DataAnnotations;

namespace Web.Models.CashgameModels.Add{

	public class AddCashgamePostModel {

        [Required(ErrorMessage = "Location can't be empty")]
	    public string Location { get; set; }

	}

}