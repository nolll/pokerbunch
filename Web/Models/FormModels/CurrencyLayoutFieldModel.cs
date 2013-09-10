using System.Collections.Generic;
using System.Linq;
using Infrastructure.System;

namespace Web.Models.FormModels{
    public class CurrencyLayoutFieldModel : SelectFieldModel{

		public CurrencyLayoutFieldModel(string fieldName, string fieldId, string selectedValue, string firstItemText = null)
            : base(fieldName, fieldId, selectedValue, GetSelectItems(), firstItemText)
        {
		}

		private static IEnumerable<SelectFieldItem> GetSelectItems(){
			var layouts = Globalization.GetCurrencyLayouts();
			var items = new List<SelectFieldItem>();
			if(layouts != null){
			    items.AddRange(layouts.Select(layout => new SelectFieldItem(layout)));
			}
			return items;
		}

	}

}