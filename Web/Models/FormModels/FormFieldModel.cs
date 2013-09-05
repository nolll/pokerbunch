namespace Web.Models.FormModels{

	public class FormFieldModel{

	    public string FieldName { get; set; }
	    public string Value { get; set; }
	    public string FieldId { get; set; }

		public FormFieldModel(string fieldName, string fieldId, string selectedValue){
			FieldName = fieldName ?? string.Empty;
			Value = selectedValue;
			FieldId = fieldId;
		}

	}

}