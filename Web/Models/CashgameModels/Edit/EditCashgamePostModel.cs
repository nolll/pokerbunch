using Web.Annotations;

namespace Web.Models.CashgameModels.Edit
{
	public class EditCashgamePostModel
    {
        public string TypedLocation { get; [UsedImplicitly] set; }
	    public string SelectedLocation { get; [UsedImplicitly] set; }

	    public string Location
	    {
	        get { return HasTypedLocation ? TypedLocation : SelectedLocation; }
	    }

	    private bool HasTypedLocation
	    {
            get { return !string.IsNullOrEmpty(TypedLocation); }
	    }
    }
}