using System.Globalization;

namespace Web.Models.ChartModels
{
    public class ChartIntValueModel : ChartValueModel
    {
        public ChartIntValueModel(int? val)
            : base(val.HasValue ? val.Value.ToString(CultureInfo.InvariantCulture) : null)
        {
        }
    }
}