using Core.UseCases;
using Web.Models.CashgameModels.Matrix;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.Details
{
	public class EventDetailsPageModel : BunchPageModel
    {
        public string Name { get; private set; }
        public CashgameMatrixTableModel MatrixModel { get; private set; }

	    public EventDetailsPageModel(BunchContext.Result contextResult, EventDetails.Result eventDetails, Matrix.Result matrixResult)
            : base(GetBrowserTitle(eventDetails), contextResult)
	    {
            Name = eventDetails.Name;
            MatrixModel = new CashgameMatrixTableModel(matrixResult);
	    }

	    private static string GetBrowserTitle(EventDetails.Result eventDetails)
	    {
	        return string.Format("Event - {0}", eventDetails.Name);
	    }
    }
}