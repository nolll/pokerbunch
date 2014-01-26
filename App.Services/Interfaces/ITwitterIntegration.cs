using Core.Classes;

namespace App.Services.Interfaces{

	public interface ITwitterIntegration{

        string GetAuthUrl();
	    TwitterCredentials GetCredentials(string token, string verifier);

	}

}