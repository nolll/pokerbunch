using System.Collections.Generic;

namespace Web.Models.ChartModels{

	public class ChartModel {
        
	    public IList<ChartColumnModel> cols { get; set; }
        public IList<ChartRowModel> rows { get; set; }
        public string p { get; set; }

        public ChartModel(){
            cols = new List<ChartColumnModel>();
            rows = new List<ChartRowModel>();
			p = null;
		}

	}

}