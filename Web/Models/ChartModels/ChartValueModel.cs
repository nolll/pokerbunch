namespace Web.Models.ChartModels{
    public class ChartValueModel {

	    public string v { get; set; }
	    public string f { get; private set; }

        public ChartValueModel()
        {
            f = null;
        }

	}

}