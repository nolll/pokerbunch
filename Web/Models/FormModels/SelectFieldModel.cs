using System.Collections.Generic;

namespace Web.Models.FormModels
{
	public class SelectFieldModel : FormFieldModel
    {
	    public List<string> Names { get; private set; }
        public List<string> Values { get; private set; }

	    protected SelectFieldModel(string fieldName, string fieldId, string selectedValue, IEnumerable<SelectFieldItem> items = null, string firstItemText = null)
            : base(fieldName, fieldId, selectedValue)
        {
			InitArrays();
			SetFirstItem(firstItemText);
			SetItems(items);
		}

		private void InitArrays()
        {
			Names = new List<string>();
			Values = new List<string>();
		}

		private void SetFirstItem(string firstItemText)
        {
		    if (string.IsNullOrEmpty(firstItemText))
                return;
		    Names.Add(firstItemText);
		    Values.Add(string.Empty);
		}

		private void SetItems(IEnumerable<SelectFieldItem> items)
		{
		    if (items == null)
                return;
		    foreach (var item in items)
            {
		        Names.Add(item.Name);
		        Values.Add(item.Value);
		    }
		}
	}
}