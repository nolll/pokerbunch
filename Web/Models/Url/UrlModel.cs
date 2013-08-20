namespace Web.Models.Url{

	public class UrlModel{

	    public string Url { get; private set; }

        protected UrlModel(){}

	    public UrlModel(string url)
	    {
	        SetUrl(url);
	    }

        protected void SetUrl(string url)
        {
            Url = url;
        }

        public override string ToString()
        {
            return Url;
        }

	}

}