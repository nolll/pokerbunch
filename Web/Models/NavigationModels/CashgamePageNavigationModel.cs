namespace Web.Models.NavigationModels{

	public class CashgamePageNavigationModel
    {
        public CashgamePage Selected { get; set; }
	    public string MatrixLink { get; set; }
        public string MatrixSelectedClass { get; set; }
        public string ToplistLink { get; set; }
        public string ToplistSelectedClass { get; set; }
        public string ChartLink { get; set; }
        public string ChartSelectedClass { get; set; }
        public string ListLink { get; set; }
        public string ListSelectedClass { get; set; }
        public string FactsLink { get; set; }
        public string FactsSelectedClass { get; set; }
    }
}
