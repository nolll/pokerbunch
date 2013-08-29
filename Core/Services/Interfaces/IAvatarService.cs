namespace Core.Services{

	public interface IAvatarService{

		string getSmallAvatarUrl(string email);
		string getLargeAvatarUrl(string email);

	}

}