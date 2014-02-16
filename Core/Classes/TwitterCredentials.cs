namespace Core.Classes
{
	public class TwitterCredentials
    {
	    public string Key { get; private set; }
        public string Secret { get; private set; }
        public string TwitterName { get; private set; }

	    public TwitterCredentials(
            string key, 
            string secret, 
            string twitterName)
	    {
	        Key = key;
	        Secret = secret;
	        TwitterName = twitterName;
	    }
	}
}