using Web.Annotations;

namespace Web.Models.CashgameModels.Edit
{
	public class CashgameEditPostModel
    {
        public string TypedLocation { get; [UsedImplicitly] set; }
	    public string SelectedLocation { get; [UsedImplicitly] set; }

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