namespace Web.Models{

	public class GoogleAnalyticsModel{

	    public bool EnableAnalytics { get; private set; }

	    public GoogleAnalyticsModel()
	    {
	        EnableAnalytics = IsInProduction();
	    }
        
		private bool IsInProduction()
		{
		    return false; //Environment::getHost() == 'pokerbunch.com';
		}

	}

}