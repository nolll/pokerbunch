using System.Collections.Generic;

namespace Web.Models.ChartModels{
    public class ChartRowModel {

		public List<ChartValueModel> C { get; set; }

		public ChartRowModel(){
			C = new List<ChartValueModel>();
		}

		public void AddValue(ChartValueModel val){
			C.Add(val);
		}

	}

}