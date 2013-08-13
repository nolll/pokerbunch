namespace Core.Classes{

	class CurrencySettings{

	    public string Symbol { get; private set; }
	    public string Layout { get; private set; }

	    public CurrencySettings(string symbol, string layout)
	    {
	        Symbol = symbol;
	        Layout = layout;
	    }

	}

}