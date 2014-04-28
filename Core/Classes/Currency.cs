namespace Core.Classes
{
	public class Currency
    {
	    public string Symbol { get; private set; }
	    public string Layout { get; private set; }

	    private Currency()
	    {
	        Symbol = "";
	        Layout = "";
	    }

	    public Currency(string symbol, string layout)
	    {
	        Symbol = symbol;
	        Layout = layout;
	    }

        public static Currency Default
        {
            get
            {
                return new Currency();
            }
        }
	}
}