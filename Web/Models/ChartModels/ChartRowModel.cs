using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

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