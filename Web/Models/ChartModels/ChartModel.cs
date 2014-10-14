using System.Collections.Generic;
using Newtonsoft.Json;
using Web.Annotations;

namespace Web.Models.ChartModels
{
	public class ChartModel
    {
        [UsedImplicitly]
        [JsonProperty("cols")]
	    public IList<ChartColumnModel> Columns { get; set; }

        [UsedImplicitly]
        [JsonProperty("rows")]
        public IList<ChartRowModel> Rows { get; set; }

        [UsedImplicitly]
        [JsonProperty("p")]
        public string P { get; set; }

        public ChartModel()
            : this(new List<ChartColumnModel>(), new List<ChartRowModel>())
        {
		}

        public ChartModel(IList<ChartColumnModel> columns, IList<ChartRowModel> rows)
        {
            Columns = columns;
            Rows = rows;
            P = null;
        }
	}
}