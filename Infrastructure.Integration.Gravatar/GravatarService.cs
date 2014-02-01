using Application.Services;
using Core.Classes;

namespace Infrastructure.Integration.Gravatar{

	public class GravatarService : IAvatarService
    {
	    private readonly ISettings _settings;
	    private readonly IEncryptionService _encryptionService;

	    public GravatarService(ISettings settings, IEncryptionService encryptionService)
	    {
	        _settings = settings;
	        _encryptionService = encryptionService;
	    }

	    public string GetSmallAvatarUrl(string email){
			return GetGravatarUrl(email, AvatarSize.Small);
		}

		public string GetLargeAvatarUrl(string email){
			return GetGravatarUrl(email, AvatarSize.Large);
		}

		private string GetGravatarUrl(string email, AvatarSize size){
            const string urlFormat = "http://www.gravatar.com/avatar/{0}?s={1}&d={2}";
		    var hash = _encryptionService.GetMd5Hash(email);
		    var pixelSize = GetPixelSize(size);
		    var defaultImageUrl = GetDefaultImageUrl();

		    return string.Format(urlFormat, hash, pixelSize, defaultImageUrl);
		}

	    private int GetPixelSize(AvatarSize size)
        {
            return size == AvatarSize.Large ? 100 : 40;
        }

	    private string GetDefaultImageUrl(){
			return _settings.GetSiteUrl() + "/FrontEnd/Images/pix.gif";

		}

	}

}