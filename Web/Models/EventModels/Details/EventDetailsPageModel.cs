using Core.UseCases.BunchContext;
using Core.UseCases.EventDetails;
using Core.UseCases.Matrix;
using Web.Models.CashgameModels.Matrix;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.Details
{
	public class EventDetailsPageModel : BunchPageModel
    {
        public string Name { get; private set; }
        public CashgameMatrixTableModel MatrixModel { get; private set; }

	    public EventDetailsPageModel(BunchContextResult contextResult, EventDetailsOutput eventDetailsOutput, MatrixResult matrixResult)
            : base(GetBrowserTitle(eventDetailsOutput), contextResult)
	    {
            Name = eventDetailsOutput.Name;
            MatrixModel = new CashgameMatrixTableModel(matrixResult);
	    }

	    private static string GetBrowserTitle(EventDetailsOutput eventDetailsOutput)
	    {
	        return string.Format("Event - {0}", eventDetailsOutput.Name);
	    }
    }
}