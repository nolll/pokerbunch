using Newtonsoft.Json;

namespace Web.Models.ChartModels
{
    public class ChartValueModel
    {
	    [JsonProperty("v")]
        public string V { get; set; }
        
        [JsonProperty("f")]
        public string F { get; private set; }

        public ChartValueModel()
        {
            F = null;
        }
	}
}