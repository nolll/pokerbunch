using System.Collections.Generic;
using Newtonsoft.Json;

namespace Web.Models.ChartModels
{
	public class ChartModel
    {
        [JsonProperty("cols")]
	    public IList<ChartColumnModel> Columns { get; set; }

        [JsonProperty("rows")]
        public IList<ChartRowModel> Rows { get; set; }

        [JsonProperty("p")]
        public string P { get; set; }

        public ChartModel(){
            Columns = new List<ChartColumnModel>();
            Rows = new List<ChartRowModel>();
			P = null;
		}
	}
}