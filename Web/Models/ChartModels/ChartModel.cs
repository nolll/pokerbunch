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