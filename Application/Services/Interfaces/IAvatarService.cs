namespace Application.Services
{
	public interface IAvatarService
    {
        string GetSmallAvatarUrl(string email);
		string GetLargeAvatarUrl(string email);
	}
}