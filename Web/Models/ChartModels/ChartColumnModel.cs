namespace Web.Models.ChartModels{

	public class ChartColumnModel {

		public string type { get; set; }
		public string label { get; set; }
	    public string pattern { get; set; }

	    public ChartColumnModel(string type, string label, string pattern = null){
			this.type = type;
			this.label = label;
			this.pattern = pattern;
		}

	}

}