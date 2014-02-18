using Newtonsoft.Json;

namespace Web.Models.ChartModels
{
	public class ChartColumnModel
    {
        [JsonProperty("type")]
		public string Type { get; private set; }

        [JsonProperty("label")]
        public string Label { get; private set; }

        [JsonProperty("pattern")]
	    public string Pattern { get; private set; }

	    public ChartColumnModel(string type, string label, string pattern = null)
        {
			Type = type;
			Label = label;
			Pattern = pattern;
		}
	}
}