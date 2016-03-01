using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Web.Models.ChartModels
{
    public class ChartValueModel
    {
        [UsedImplicitly]
	    [JsonProperty("v")]
        public string V { [UsedImplicitly] get; set; }

        [UsedImplicitly]
        [JsonProperty("f")]
        public string F { [UsedImplicitly] get; private set; }

        public ChartValueModel(string val)
        {
            V = val;
            F = null;
        }
    }
}