using Core.Classes;

namespace Application.Services{

	public interface ITwitterIntegration{

        string GetAuthUrl();
	    TwitterCredentials GetCredentials(string token, string verifier);

	}

}