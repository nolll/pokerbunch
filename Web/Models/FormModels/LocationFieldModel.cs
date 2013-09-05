using System.Collections.Generic;
using System.Linq;

namespace Web.Models.FormModels{

	public class LocationFieldModel : ListFieldModel{

		public LocationFieldModel(string fieldName, string fieldId, string selectedValue, IEnumerable<string> locations, string firstItemText = null)
            : base(fieldName, fieldId, selectedValue, GetSelectItems(locations), firstItemText)
        {
		}

		private static IEnumerable<SelectFieldItem> GetSelectItems(IEnumerable<string> locations)
		{
		    var items = new List<SelectFieldItem>();
			if(locations != null){
			    items.AddRange(locations.Select(location => new SelectFieldItem(location)));
			}
			return items;
		}

	}

}