namespace Application.Services
{
	public class GravatarService : IAvatarService
    {
        private readonly EncryptionService _encryptionService = new EncryptionService();

		public string GetAvatarUrl(string email)
        {
            const string urlFormat = "http://www.gravatar.com/avatar/{0}?s=100";
		    var hash = _encryptionService.GetMd5Hash(email);

		    return string.Format(urlFormat, hash);
		}
	}
}