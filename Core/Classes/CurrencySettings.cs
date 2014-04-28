namespace Core.Classes
{
	public class CurrencySettings
    {
	    public string Symbol { get; private set; }
	    public string Layout { get; private set; }

	    private CurrencySettings()
	    {
	        Symbol = "";
	        Layout = "";
	    }

	    public CurrencySettings(string symbol, string layout)
	    {
	        Symbol = symbol;
	        Layout = layout;
	    }

        public static CurrencySettings Default
        {
            get
            {
                return new CurrencySettings();
            }
        }
	}
}