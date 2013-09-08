using System.Collections.Generic;

namespace Web.Models.ChartModels{

	public class ChartModel {
        
	    public List<ChartColumnModel> cols { get; set; }
        public List<ChartRowModel> rows { get; set; }
        public string p { get; set; }

        public ChartModel(){
            cols = new List<ChartColumnModel>();
            rows = new List<ChartRowModel>();
			p = null;
		}

		protected void AddColumn(ChartColumnModel col){
			cols.Add(col);
		}

		protected void AddRow(ChartRowModel row){
			rows.Add(row);
		}

	}

}