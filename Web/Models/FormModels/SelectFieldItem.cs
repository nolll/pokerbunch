namespace Web.Models.FormModels{
    public class SelectFieldItem{

	    public string Name { get; set; }
	    public string Value { get; set; }

		public SelectFieldItem(string name, string value = null){
			Name = name;
			Value = value ?? name;
		}

	}

}