using Core.Classes;

namespace Core.Services{

	public interface ITwitterIntegration{

        string GetAuthUrl();
	    TwitterCredentials GetCredentials(string token, string verifier);

	}

}