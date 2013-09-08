using System.Globalization;

namespace Web.Models.ChartModels{
    public class ChartValueModel {

	    public string v { get; set; }
	    public string f { get; set; }

        public ChartValueModel(string val)
        {
            v = val;
            f = null;
        }

        public ChartValueModel(int val)
            : this(val.ToString(CultureInfo.InvariantCulture))
        {
        }

	}

}