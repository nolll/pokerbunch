using Web.Annotations;

namespace Web.Models.CashgameModels.Add
{
	public class AddCashgamePostModel
    {
	    public string TypedLocation { get; [UsedImplicitly] set; }
	    public string SelectedLocation { get; [UsedImplicitly] set; }

	    public string Location
	    {
	        get { return TypedLocation ?? SelectedLocation; }
	    }
	}
}