namespace Web.Models.CashgameModels.Add{

	public class AddCashgamePostModel {

	    public string Location { get; set; }

	    public AddCashgamePostModel() {}

	    public AddCashgamePostModel(string textBoxValue, string dropDownValue)
	    {
	        var location = textBoxValue;
			if(string.IsNullOrEmpty(location)){
				location = dropDownValue;
			}
	        Location = location;
	    }

	}

}