using System.Globalization;

namespace Core.Classes
{
	public class Currency
	{
	    public string Symbol { get; private set; }
	    public string Layout { get; private set; }
	    public CultureInfo Culture { get; set; }

	    public Currency(string symbol, string layout, CultureInfo culture = null)
	    {
	        Symbol = symbol;
	        Layout = layout;
	        Culture = culture ?? CultureInfo.CreateSpecificCulture("sv-SE");
	    }

        public static Currency Default
        {
            get
            {
                return new Currency("", "");
            }
        }
	}
}