namespace App.Services.Interfaces{

	public interface IAvatarService
    {
        string GetSmallAvatarUrl(string email);
		string GetLargeAvatarUrl(string email);

	}

}