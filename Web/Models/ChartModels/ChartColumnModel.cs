namespace Web.Models.ChartModels{

	public class ChartColumnModel {

		public string Type { get; set; }
		public string Label { get; set; }
	    public string Pattern { get; set; }

	    public ChartColumnModel(string type, string label, string pattern = null){
			Type = type;
			Label = label;
			Pattern = pattern;
		}

	}

}