using System.Collections.Generic;

namespace Web.Models.ChartModels{

	public class ChartModel {
        
	    public List<ChartColumnModel> Cols { get; set; }

        public List<ChartRowModel> Rows { get; set; }

	    public string P { get; set; }

        public ChartModel(){
            Cols = new List<ChartColumnModel>();
            Rows = new List<ChartRowModel>();
			P = null;
		}

		protected void AddColumn(ChartColumnModel col){
			Cols.Add(col);
		}

		protected void AddRow(ChartRowModel row){
			Rows.Add(row);
		}

	}

}