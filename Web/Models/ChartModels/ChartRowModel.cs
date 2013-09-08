using System.Collections.Generic;

namespace Web.Models.ChartModels{
    public class ChartRowModel {

		public List<ChartValueModel> c { get; set; }

		public ChartRowModel(){
			c = new List<ChartValueModel>();
		}

		public void AddValue(ChartValueModel val){
			c.Add(val);
		}

	}

}