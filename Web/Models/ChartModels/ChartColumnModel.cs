using Newtonsoft.Json;
using Web.Annotations;

namespace Web.Models.ChartModels
{
	public class ChartColumnModel
    {
        [UsedImplicitly]
        [JsonProperty("type")]
		public string Type { get; private set; }

        [UsedImplicitly]
        [JsonProperty("label")]
        public string Label { get; private set; }

        [UsedImplicitly]
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