using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Core.Classes;
using Core.Services;
using Infrastructure.Config;

namespace Infrastructure.Integration.Gravatar{

	public class GravatarService : IAvatarService{
	    private readonly ISettings _settings;

	    public GravatarService(ISettings settings)
	    {
	        _settings = settings;
	    }

	    public string getSmallAvatarUrl(string email){
			return getGravatarUrl(email, AvatarSize.Small);
		}

		public string getLargeAvatarUrl(string email){
			return getGravatarUrl(email, AvatarSize.Large);
		}

		private string getGravatarUrl(string email, AvatarSize size){
            const string urlFormat = "http://www.gravatar.com/avatar/{0}?s={1}&d={2}";
		    var hash = GetHash(email);
		    var pixelSize = GetPixelSize(size);
		    var defaultImageUrl = GetDefaultImageUrl();

		    return string.Format(urlFormat, hash, pixelSize, defaultImageUrl);
		}

	    private string GetHash(string input)
	    {
            var sb = new StringBuilder();
	        using (var md5 = MD5.Create())
	        {
                byte[] inputBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input.Trim()));
	            foreach (var inputByte in inputBytes)
	            {
                    sb.Append(inputByte.ToString("x2"));
	            }
	        }
	        return sb.ToString();
	    }

	    private int GetPixelSize(AvatarSize size)
        {
            return size == AvatarSize.Large ? 100 : 40;
        }

	    private string GetDefaultImageUrl(){
			return _settings.GetSiteUrl() + "/core/ui/img/pix.gif";

		}

	}

}