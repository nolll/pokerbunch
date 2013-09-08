using System.Globalization;

namespace Web.Models.ChartModels{
    public class ChartValueModel {

	    public string v { get; set; }
	    public string f { get; set; }

        public ChartValueModel()
        {
            f = null;
        }

        public ChartValueModel(string val) : this()
        {
            v = val;
        }

        public ChartValueModel(int? val) : this()
        {
            v = val.HasValue ? val.Value.ToString(CultureInfo.InvariantCulture) : null;
        }

	}

}