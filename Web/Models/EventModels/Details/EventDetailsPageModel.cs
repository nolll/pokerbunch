using Core.UseCases;
using Web.Extensions;
using Web.Models.CashgameModels.Matrix;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.Details
{
	public class EventDetailsPageModel : BunchPageModel
    {
	    private readonly EventDetails.Result _eventDetails;
	    public string Name { get; }
        public CashgameMatrixTableModel MatrixModel { get; }

	    public EventDetailsPageModel(BunchContext.Result contextResult, EventDetails.Result eventDetails, Matrix.Result matrixResult)
            : base(contextResult)
	    {
	        _eventDetails = eventDetails;
	        Name = _eventDetails.Name;
            MatrixModel = new CashgameMatrixTableModel(matrixResult);
	    }

	    public override string BrowserTitle => $"Event - {_eventDetails.Name}";

	    public override View GetView()
	    {
	        return new View("~/Views/Pages/EventDetails/EventDetails.cshtml");
	    }
    }
}