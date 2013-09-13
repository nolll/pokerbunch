namespace Core.Classes{

	public class CurrencySettings{

	    public string Symbol { get; set; }
	    public string Layout { get; set; }

	    public CurrencySettings()
	    {
	        
	    }

	    public CurrencySettings(string symbol, string layout)
	    {
	        Symbol = symbol;
	        Layout = layout;
	    }

	}

}