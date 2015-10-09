using System.Collections.Generic;
using Newtonsoft.Json;
using Web.Annotations;

namespace Web.Models.ChartModels
{
	public abstract class ChartModel
    {
        [UsedImplicitly]
        [JsonProperty("colors")]
        public IList<string> Colors { get; private set; }

        [UsedImplicitly]
        [JsonProperty("cols")]
	    public IList<ChartColumnModel> Columns { get; private set; }

        [UsedImplicitly]
        [JsonProperty("rows")]
        public IList<ChartRowModel> Rows { get; private set; }

        [UsedImplicitly]
        [JsonProperty("p")]
        public string P { get; private set; }

	    protected ChartModel(IList<ChartColumnModel> columns, IList<ChartRowModel> rows, IList<string> colors = null)
	    {
	        Colors = colors;
            Columns = columns;
            Rows = rows;
            P = null;
        }
	}
}