using System.Globalization;

namespace Web.Models.ChartModels{
    public class ChartValueModel {

	    public string V { get; set; }
	    public string F { get; set; }

        public ChartValueModel(string val)
        {
            V = val;
            F = null;
        }

        public ChartValueModel(int val)
            : this(val.ToString(CultureInfo.InvariantCulture))
        {
        }

	}

}