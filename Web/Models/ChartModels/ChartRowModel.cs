using System.Collections.Generic;
using Newtonsoft.Json;

namespace Web.Models.ChartModels
{
    public class ChartRowModel
    {
        [JsonProperty("c")]
		public List<ChartValueModel> C { get; set; }

		public ChartRowModel(){
			C = new List<ChartValueModel>();
		}
	}
}