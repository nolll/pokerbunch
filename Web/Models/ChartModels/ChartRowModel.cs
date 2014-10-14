using System.Collections.Generic;
using Newtonsoft.Json;
using Web.Annotations;

namespace Web.Models.ChartModels
{
    public class ChartRowModel
    {
        [UsedImplicitly]
        [JsonProperty("c")]
		public IList<ChartValueModel> C { get; set; }

        public ChartRowModel(IList<ChartValueModel> valueModels)
        {
            C = valueModels;
        }
	}
}