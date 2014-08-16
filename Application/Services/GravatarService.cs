using Core.Entities;

namespace Application.Services
{
	public class GravatarService : IAvatarService
    {
        private readonly EncryptionService _encryptionService = new EncryptionService();

	    public string GetSmallAvatarUrl(string email)
        {
			return GetGravatarUrl(email, AvatarSize.Small);
		}

		public string GetLargeAvatarUrl(string email)
        {
			return GetGravatarUrl(email, AvatarSize.Large);
		}

		private string GetGravatarUrl(string email, AvatarSize size)
        {
            const string urlFormat = "http://www.gravatar.com/avatar/{0}?s={1}";
		    var hash = _encryptionService.GetMd5Hash(email);
		    var pixelSize = GetPixelSize(size);

		    return string.Format(urlFormat, hash, pixelSize);
		}

	    private int GetPixelSize(AvatarSize size)
        {
            return size == AvatarSize.Large ? 100 : 40;
        }
	}
}