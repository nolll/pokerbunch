using System.Collections.Generic;

namespace Web.Models.FormModels{

	public class ListFieldModel : SelectFieldModel{

	    public string ListName { get; set; }
	    public string DropDownName { get; set; }

		public ListFieldModel(string fieldName, string fieldId, string selectedValue, IEnumerable<SelectFieldItem> items = null, string firstItemText = null)
            : base(fieldName, fieldId, selectedValue, items, firstItemText)
        {
            ListName = string.Format("{0}-list", fieldName);
            DropDownName = string.Format("{0}-dropdown", fieldName);
		}

	}

}