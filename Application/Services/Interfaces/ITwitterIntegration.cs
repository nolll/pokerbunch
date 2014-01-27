using Core.Classes;

namespace Application.Services.Interfaces{

	public interface ITwitterIntegration{

        string GetAuthUrl();
	    TwitterCredentials GetCredentials(string token, string verifier);

	}

}