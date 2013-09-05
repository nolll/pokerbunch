namespace Web.Models.CashgameModels.Add{

	public class AddPostModel {

	    public string Location { get; set; }

	    public AddPostModel() {}

	    public AddPostModel(string textBoxValue, string dropDownValue)
	    {
	        var location = textBoxValue;
			if(string.IsNullOrEmpty(location)){
				location = dropDownValue;
			}
	        Location = location;
	    }

        /*
		public AddPostModel(Request $request){
			location = $request.getParamPost('location');
			if(location == null || location == ''){
				location = $request.getParamPost('location-dropdown');
			}
		}
        */

	}

}