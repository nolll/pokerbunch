namespace Web.Models.CashgameModels.Add{

	public class AddCashgamePostModel {

	    public string TypedLocation { get; set; }
	    public string SelectedLocation { get; set; }

	    public bool HasLocation
	    {
	        get { return !string.IsNullOrEmpty(Location); }
	    }

	    public string Location
	    {
	        get { return TypedLocation ?? SelectedLocation; }
	    }

	}

}